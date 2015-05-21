#include "Kernel.h"

Stone::Stone(GoColor c, int i, int j)
{
	m_c = c;
	m_i = i;
	m_j = j;
	m_g = 0;
}

Group *Stone::getGroup()
{
	return m_g;
}

GoColor Stone::getColor()
{
	return m_c;
}

void Stone::setGroup(Group *g)
{
	m_g = g;
}

std::pair<int, int> Stone::getPosition()
{
	std::pair<int, int> ret;
	ret.first = m_i;
	ret.second = m_j;
	return ret;
}