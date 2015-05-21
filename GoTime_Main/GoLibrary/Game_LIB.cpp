#include "Stdafx.h"
#include "GoLibrary.h"

using namespace GoLibrary;

Game_LIB::Game_LIB(int s)
{
	game = new Game(s);
}

Game_LIB::~Game_LIB()
{
	delete game;
}

ReturnCodes_LIB Game_LIB::placeStone(int i, int j)
{
	return static_cast<ReturnCodes_LIB>(game->placeStone(i,j));
}

GoColor_LIB Game_LIB::query(int i, int j)
{
	return static_cast<GoColor_LIB>(game->query(i,j));
}