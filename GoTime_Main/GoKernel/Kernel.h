#ifndef KERNEL_H
#define KERNEL_H

#include <vector>

enum GoColor {NONE=0,BLACK=1,WHITE=2};
enum ReturnCodes {OUT_OF_BOUNDS=0,STONE_PRESENT=1,ILLEGAL_KO=2,SUICIDE=3,OK=4};

class Stone;

class Group
{
private:
	std::vector<Stone *> stones;
	std::vector<std::pair<int, int>> liberties;
public:
	int numberOfStones();
	int numberOfLiberties();
	std::pair<int, int> getLiberty(int i);
	Stone *getStone(int i);
	GoColor getColor();
	void addStone(Stone *s); //should also set group pointer in the stone
	void addLiberty(int i, int j);
	bool removeLiberty(int i, int j);
};

class Stone
{
private:
	GoColor m_c;
	int m_i, m_j;
	Group *m_g;
public:
	Stone(GoColor c, int i, int j);
	Group *getGroup();
	GoColor getColor();
	void setGroup(Group *g);
	std::pair<int, int> getPosition();
};

class HistoryEntry
{
private:
	bool m_pass;
	int m_i, m_j;
	std::vector<std::vector<std::pair<int, int>>> m_capturedGroups;
public:
	HistoryEntry(bool pass, int i, int j,
		std::vector<std::vector<std::pair<int, int>>> capturedGroups);
	std::vector<std::pair<int, int>> getCapturedGroup(int i);
	int numberOfCapturedGroups();
	std::pair<int, int> getPlay();
};

class Game
{
private:
	int side;
	Stone ***board;
	GoColor firstMove;
	//turn 0 is the empty board (or board with handicap stones),
	//and ends when the first player makes a play
	int turn;
	std::vector<HistoryEntry *> history;

	bool koCheck(int i, int j);
	bool suicideCheck(int i, int j,
		std::vector<Group *> buddies, std::vector<Group *> enemies);
	std::pair<std::vector<Group *>, std::vector<Group *>> findAdjGroups(int i, int j);
	void uniteGroups(int i, int j, std::vector<Group *> buddies);
	std::vector<std::vector<std::pair<int, int>>>
		capture(int i, int j, std::vector<Group *> enemies);
	GoColor whoseTurn(int t);
	GoColor whoseTurn();
public:
	Game(int s);
	~Game();
	ReturnCodes placeStone(int i, int j);
	GoColor query(int i, int j);
};

#endif