using System;

public static class GameState
{
	public static string loadMsg = "Overslept again. I need coffee.";
	public static float TRANSTIME = .1f;

	public static bool canClickOnMug        = true;
	public static bool canClickOnBrokenMug  = true;
	public static bool canClickOnPhoto		= false;
	public static bool canClickOnPlatty		= false;
	public static bool canClickOnKeys		= false;
	public static bool canClickOnBriefcase	= false;
    public static bool canClickOnNormalBriefcase = false;
    public static bool canClickOnMeds		= false;
	public static bool canClickOnNote		= false;
	public static bool canDropOnStove		= false;

	public static bool showPlatty			= false;
	public static bool showMeds				= false;
	public static bool showStoveOn			= false;
	public static bool showLargeMeds		= false;
	public static bool showBriefcase		= false;
	public static bool showNote				= false;
    public static bool showGoodHallway = true;


    public static bool hasMedsInHand = false;
    public static bool tookMeds = false;
    public static bool readyToLeave = false;

	public static string zoomImage = "";

}


