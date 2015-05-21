// GoLibrary.h

#pragma once
#include "..\GoKernel\Kernel.h"

using namespace System;

namespace GoLibrary {
	public enum class GoColor_LIB {NONE=0,BLACK=1,WHITE=2};
	public enum class ReturnCodes_LIB {OUT_OF_BOUNDS=0,STONE_PRESENT=1,ILLEGAL_KO=2,SUICIDE=3,OK=4};

	public ref class Game_LIB
	{
		Game *game;

	public:
		Game_LIB(int s);
		~Game_LIB();
		ReturnCodes_LIB placeStone(int i, int j);
		GoColor_LIB query(int i, int j);
	};
}