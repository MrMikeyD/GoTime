#include "Kernel.h"

HistoryEntry::HistoryEntry(bool pass, int i, int j,
	std::vector<std::vector<std::pair<int, int>>> capturedGroups)
{
	m_pass = pass;
	m_i = i;
	m_j = j;
	m_capturedGroups = capturedGroups;
}

std::vector<std::pair<int, int>> HistoryEntry::getCapturedGroup(int i)
{
	return m_capturedGroups[i];
}

int HistoryEntry::numberOfCapturedGroups()
{
	return m_capturedGroups.size();
}

std::pair<int, int> HistoryEntry::getPlay()
{
	std::pair<int, int> ret;
	ret.first = m_i;
	ret.second = m_j;
	return ret;
}