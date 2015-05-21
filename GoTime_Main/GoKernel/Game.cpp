#include "Kernel.h"

Game::Game(int s)
{
	//initialize simple things
	side = s;
	turn = 0;
	firstMove = GoColor::BLACK;

	//initialize board
	board = new Stone **[s];
	for (int i=0; i<s; i++)
	{
		board[i] = new Stone *[s];
		for (int j=0; j<s; j++) board[i][j] = 0;
	}
}

Game::~Game()
{
	for (int i=0; i<side; i++)
	{
		for (int j=0; j<side; j++)
		{
			if (board[i][j]==0) continue;
			if (board[i][j]->getGroup() != 0) {
				delete board[i][j]->getGroup();
				board[i][j]->setGroup(0);
			}
			delete board[i][j];
		}
		delete[] board[i];
	}
	for (unsigned int i=0; i<history.size(); i++)
		delete history[i];
	delete[] board;
}

bool Game::koCheck(int i, int j)
{
	std::vector<std::pair<int, int>> grp_captured;
	std::pair<int, int> play;
	Group *grp_played;

	if (turn == 0)
		return false;
	if (history[turn - 1]->numberOfCapturedGroups() != 1)
		return false;
	grp_captured = history[turn - 1]->getCapturedGroup(0);
	if (grp_captured.size() != 1)
		return false;
	if (grp_captured[0].first != i || grp_captured[0].second != j)
		return false;
	play = history[turn - 1]->getPlay();
	grp_played = board[play.first][play.second]->getGroup();
	if (grp_played->numberOfStones() != 1 || grp_played->numberOfLiberties() != 1)
		return false;
	//grp_played's one liberty must be (i, j)
	//the play at (i, j) only captures one stone
	return true;
}

bool Game::suicideCheck(int i, int j,
	std::vector<Group *> buddies, std::vector<Group *> enemies)
{
	if ((i != 0 && board[i-1][j] == 0) || (j != 0 && board[i][j-1] == 0) ||
		(j != side-1 && board[i][j+1] == 0) || (i != side-1 && board[i+1][j] == 0))
		return false;
	for (unsigned int k = 0; k < enemies.size(); k++) {
		if (enemies[k]->numberOfLiberties() == 1)
			return false;
	}
	for (unsigned int k = 0; k < buddies.size(); k++) {
		if (buddies[k]->numberOfLiberties() > 1)
			return false;
	}
	return true;
}

//find groups that have a stone in one of the up to four
//locations adjacent to (i, j). these groups are returned
//in two lists differentiated by the color of the group.
std::pair<std::vector<Group *>, std::vector<Group *>>
Game::findAdjGroups(int i, int j)
{
	std::pair<std::vector<Group *>, std::vector<Group *>> ret;
	std::vector<Group *> buddies, enemies;
	Group *up, *left, *right, *down;
	up = left = right = down = 0;

	if (i != 0 && board[i-1][j] != 0) {
		up = board[i-1][j]->getGroup();
		if (up->getColor() != whoseTurn())
			enemies.push_back(up);
		else
			buddies.push_back(up);
	}
	if (j != 0 && board[i][j-1] != 0) {
		left = board[i][j-1]->getGroup();
		if (left != up) {
			if (left->getColor() != whoseTurn())
				enemies.push_back(left);
			else
				buddies.push_back(left);
		}
	}
	if (j != side-1 && board[i][j+1] != 0) {
		right = board[i][j+1]->getGroup();
		if (right != up && right != left)
		{
			if (right->getColor() != whoseTurn())
				enemies.push_back(right);
			else
				buddies.push_back(right);
		}
	}
	if (i != side-1 && board[i+1][j] != 0) {
		down = board[i+1][j]->getGroup();
		if (down != up && down != left && down != right)
		{
			if (down->getColor() != whoseTurn())
				enemies.push_back(down);
			else
				buddies.push_back(down);
		}
	}

	ret.first = buddies;
	ret.second = enemies;
	return ret;
}

void Game::uniteGroups(int i, int j, std::vector<Group *> buddies)
{
	board[i][j] = new Stone(whoseTurn(), i, j);

	//if the play at (i, j) starts a new group
	if (buddies.size() == 0) {
		Group *g = new Group();

		g->addStone(board[i][j]);
		if (i != 0 && board[i-1][j] == 0)
			g->addLiberty(i-1,j);
		if (j != 0 && board[i][j-1] == 0)
			g->addLiberty(i,j-1);
		if (j != side-1 && board[i][j+1] == 0)
			g->addLiberty(i,j+1);
		if (i != side-1 && board[i+1][j] == 0)
			g->addLiberty(i+1,j);
	}
	//the play at (i, j) combines existing groups
	else {
		for (unsigned int k=0; k < buddies.size(); k++)
			buddies[k]->removeLiberty(i,j);
		
		buddies[0]->addStone(board[i][j]);
		if (i != 0 && board[i-1][j] == 0)
			buddies[0]->addLiberty(i-1,j);
		if (j != 0 && board[i][j-1] == 0)
			buddies[0]->addLiberty(i,j-1);
		if (j != side-1 && board[i][j+1] == 0)
			buddies[0]->addLiberty(i,j+1);
		if (i != side-1 && board[i+1][j] == 0)
			buddies[0]->addLiberty(i+1,j);

		for (unsigned int k=1; k < buddies.size(); k++) {
			for (int m=0; m < buddies[k]->numberOfStones(); m++) {
				buddies[0]->addStone(buddies[k]->getStone(m));
			}
			for (int m=0; m < buddies[k]->numberOfLiberties(); m++) {
				std::pair<int, int> liberty = buddies[k]->getLiberty(m);
				buddies[0]->addLiberty(liberty.first, liberty.second);
			}
			delete buddies[k];
		}
	}
}

std::vector<std::vector<std::pair<int, int>>>
Game::capture(int i, int j, std::vector<Group *> enemies)
{
	std::vector<std::vector<std::pair<int, int>>> ret;

	for (unsigned int k=0; k < enemies.size(); k++) {
		enemies[k]->removeLiberty(i, j);
		if (enemies[k]->numberOfLiberties() == 0) {
			std::vector<std::pair<int, int>> captured_group;

			for (int m=0; m < enemies[k]->numberOfStones(); m++) {
				int x, y;

				std::pair<int, int> captured_stone =
					enemies[k]->getStone(m)->getPosition();
				captured_group.push_back(captured_stone);
				x = captured_stone.first;
				y = captured_stone.second;

				if (x != 0) {
					Stone *s = board[x-1][y];
					if (s != 0 && s->getColor() == whoseTurn())
						s->getGroup()->addLiberty(x, y);
				}
				if (y != 0) {
					Stone *s = board[x][y-1];
					if (s != 0 && s->getColor() == whoseTurn())
						s->getGroup()->addLiberty(x, y);
				}
				if (y != side-1) {
					Stone *s = board[x][y+1];
					if (s != 0 && s->getColor() == whoseTurn())
						s->getGroup()->addLiberty(x, y);
				}
				if (x != side-1) {
					Stone *s = board[x+1][y];
					if (s != 0 && s->getColor() == whoseTurn())
						s->getGroup()->addLiberty(x, y);
				}

				board[x][y] = 0;
				delete board[x][y];
			}

			ret.push_back(captured_group);
			delete enemies[k];
		}
	}

	return ret;
}

GoColor Game::whoseTurn(int t)
{
	if (t % 2 == 0) return firstMove;
	if (firstMove == GoColor::BLACK) return GoColor::WHITE;
	return GoColor::BLACK;
}

GoColor Game::whoseTurn()
{
	return whoseTurn(turn);
}

ReturnCodes Game::placeStone(int i, int j)
{
	std::pair<std::vector<Group *>, std::vector<Group *>> adjGroups;
	std::vector<Group *> buddies, enemies;
	std::vector<std::vector<std::pair<int, int>>> captured_groups;

	//error checking, and find groups adjacent to the requested play (i, j)
	if (i<0 || i>=side || j<0 || j>=side) return ReturnCodes::OUT_OF_BOUNDS;
	if (board[i][j]!=0) return ReturnCodes::STONE_PRESENT;
	adjGroups = findAdjGroups(i,j);
	buddies = adjGroups.first;
	enemies = adjGroups.second;
	if (suicideCheck(i,j,buddies,enemies)) return ReturnCodes::SUICIDE;
	if (koCheck(i,j)) return ReturnCodes::ILLEGAL_KO;

	//make play and update current player's groups
	uniteGroups(i,j,buddies);
	//update other player's groups and make captures
	captured_groups = capture(i,j,enemies);

	//end turn
	HistoryEntry *h = new HistoryEntry(false, i, j, captured_groups);
	history.push_back(h);
	turn++;

	return ReturnCodes::OK;
}

GoColor Game::query(int i, int j)
{
	if (board[i][j] == 0)
		return GoColor::NONE;
	return board[i][j]->getColor();
}