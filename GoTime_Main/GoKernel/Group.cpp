#include "Kernel.h"

int Group::numberOfStones()
{
	return stones.size();
}

int Group::numberOfLiberties()
{
	return liberties.size();
}

std::pair<int, int> Group::getLiberty(int i)
{
	return liberties[i];
}

Stone *Group::getStone(int i)
{
	return stones[i];
}

GoColor Group::getColor()
{
	return stones[0]->getColor();
}

void Group::addStone(Stone *s)
{
	stones.push_back(s);
	s->setGroup(this);
}

void Group::addLiberty(int i, int j)
{
	std::pair<int, int> temp;

	temp.first = i;
	temp.second = j;
	for (unsigned int k=0; k < liberties.size(); k++) {
		if (i == liberties[k].first && j == liberties[k].second)
			return;
	}
	liberties.push_back(temp);
}

bool Group::removeLiberty(int i, int j)
{
	for (std::vector<std::pair<int, int>>::iterator it = liberties.begin();
		it != liberties.end(); it++) {
		if (i == it->first && j == it->second) {
			liberties.erase(it);
			return true;
		}
	}
	return false;
}