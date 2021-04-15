﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    public static class Program
    {
        public static void Main()
        {
            var subWorld = new SubWorld(SubWorld.firstWorldBarriers);
            var player = new Player(5000, subWorld);

            for (var i = 0; i < 2000; ++i)
            {
                player.Move(Directions.Right);
                Console.WriteLine(player.CurrentPosition);
            }
        }
    }
}


/*&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%###%&%%%%%#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&
&&&&&&&&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#/#%%&&&%%%##%%#%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%(((/////*,,/%&&&&&&&&&&&&&&&&&&&&&&&&&&&&
&&&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#((#&&&&&&&%%#########%%%%%%%%%%%%%%%%%%%%%%%%%%%#((////(%%%%%%%%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&%%%&&&%%&&&&%%%#(*,*(&&&&&&&&&&&&&&&&&&&&&&&&
&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#((#&&&&&&%%%##%%#####%%%%%%%%%%%%%#(((((#%%(,            .,/#%%%%%%%%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&@@@@@@@@@@@&&%%%(,#1###(((%&&&&&&&&&&&&&&&&
&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#//(&&&&&&&%%%%%%%##%%%(***#1#(#/,,,,,...     .......           ..,,*#1#((((((##%%%%&&&%(,.......,..,/*,,,#1#/**#%&@@&&%%(,*(%%%(**(&&&&&&&&&&&&&
&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#####%%%%%%%%%%%#(*(((&&@&&&&&&&&&&&%#%%%%###%%#/*,,,,,,,....,,,,,,,,,....  ........       ..........,*#&%%%%%%%%#(/#1#/,.,#1#/****.,%@@@&%%(//(&%#/**(&&&&&&&&&&&
&%%%%%%%%%%%%%%%%%%%%##%%%######%#####################%#((*%&#%@@@@@&&&&&&&&&&&&%%%%%%%%%%%#(*,,,,,....,,,,,,,,,,*,,,,,,,,,,.....,,,*****#1#(&@&&&@@@@&&&&&%%%%(*..,,,,,,****(&@@&&%%(((%&%(,,/&&&&&&&&&&
%%%%%%%%%%%%%%%%%#####################################/,,##%&&&@@&%(*.    ......,**(%&&&&%%##(((/,,,,,,.,,,,,,,,*****,,,,,,,,,,,,,**#1#///#&@@@@@@@@@@@@@@@@@&&&%%%(,*,.,*,****&@@&&%%###&&%#*#1#&&&&&&&&&
%%%%%%%%%%%%%%%######################################*.,/((/*..            .......... .#1##%%##((//*,,,,,,*,,,,********,,,,,,,,,***#1#//((#&@@@@@@&%%########%&@@@&&&%%(/***,**,/&@@&&%%%%%&&%(/*%&&&&&&&&
%%%%%%%%%%%%########################################/... ..................................,/(####(((/****,,**********,,,,,,,***#1#//(((#%&@@@&%#((((((((#%%%###&@@@&&%%#(*,*#1#//%@@&&%%%%&&%%((#(%&&&&&&
%%%%%%%%%%%%####################################(*,....,,,,,,,,,,,,,,..........,(/,,#1#/**(#/,.*%%##((#(/***#1#////******,,******#1#//((((#%&&&%##((////////****#1###%@@@&&&%#*#1#(((#@@&&&&%%&&&%%#%#/#1#&&&&
%%%%%%%%%%%%%################################(/*,...,,,,,,,,,,,,,,,,,,,,,,,,,.,((##*.     ..,(#/#1#(#%%#((/*#1#////*****,,*****#1#//////((#%%%%&&&%#((////*****,,,#1#(#&@@@&&%#(/(##%@@@@&&&&%@&&&&&#(/*,*(%
%%%%%%%%%%%%################################(/*...**********,,,*,,,,,,.,,,,,/(*...  .     ..     *#####%%&#/*#1#//*****,,,**#1#////#1####(*,*(%%&&%#(((//(((((((//////(%&@@@&&####%&&@@@@&&#/%@&&&##((///*,
%%%%%%%%%%%%%#############################(**,..,#1#/////*************,,,,#1#(*,.......    ..,,,,,,..,#&%%##%&%/#1#///***,,,*#1#////#&&&&&%%###(/#1#((((((#((///////##(//(%%&@@&&&%##%%%&@@&#((/%@&#(((((////
%%%%%%%%%%%#####################(/**,,,,,,,/....,*((#(((////////////****(#/*,,*****,,,.   ....,,,,(##*(%###(%&#**#1#****,,,#1#////(#%&@@&&&&%%%##(/**#1#(///***#1#(##%%#((#(%@@@&%%#####%%%###(/(#(((///(/(/
%%%%%%%%%%##################(*,,,,,*#1#((/**.,#1#(((((%%#(((((((//(#(*,/%#(///(((####(((//*,,..,,,,,*%%%###%#((#%/***#1#**,,,,#1#(((/(((#%&&&&@@&&&%%%%#(/,.,***#1#(((((#(((((/#&&%%%#((((######(//(((////(((
%%%%%%%%%%###############(,.,#1#((((((((#1##1###%&&&&&&%&@%%####%%%%&&%%%%%%#%%%%&&%%%%%&%%#(//*#1#*,,,,/&&&%%%%####**,*#1#/**,,*((((((((#%&&&&%#(#%&&&&&%%%%#(/*#1#/((///(##(((((%&&%%##(//(((###((///((///((
%%%%%%%%%%%###########(*,,/(((((#####//((&&&@@@@@@@@@@@@@@@%/,,,,.*#&&&&&@@&&%/*,,,..*(#%%#((////*,.,#1#%%%%%%%*((*,,,#1#//*#1#(###(((((%%&&%%%%##(((#%&&&&&%%%%####(///(##(((/(#%%%###((//(((((/////((//((
%%%%%%%%%##########(*,,/(((((#######%(#&&&&%/,,#1##&&&&&&&#*......,,,#1#(((//###(///**#1#(#%%##(/#1#****,,,(%%%%#*,**,,,,,#1#////(#%%#(((%&%%%%%%###((((%%%%&&&&&&%%%%%%#((((##((//((###((((///////(((//(((((
%%%%%%%%%%%#####(,,,#1#((((##########%##&&&%%%#(((((#%##(##%#/*****,,*#1#//((/(#(((((##(((#(/*,,,**#1#/**#1#%%%%#(#%@@&#*..,#1#/((#%%%###%&&%%######(#######%%&&&&@&&&&%&%%%%%###(((/(((((((((((/////((((((((
%%%%%%%%%%%%%#*.,#1#/((((############%#/((%&&&&%%%%%#(#%(***#1#(####%#(/*,..#1#,,,*(###((/****,,,,**#1#/*#1#/#%%%##(//(##/***..#1#((#%&%%%%&&&%%##############%&&&&&&@&&&&&&%%%%####(((/((((((((((((((((((((((
%%%%%%%%%%%#,.,#1#((((###############%#*#1#*#1##%%%#((/,,,*#1#(/**,,.  ..  ....,#1#/*,,,,,,,.,.,*****#1#/////(#####/*%%#/*((//*,,,#1#(#&&&&%&&&%%%##########(##%%&&&&&&&&&%%%%%%%%######(((((((((((((((((((((((
%%%%%%%%%(..,#1#(((#################%%#/*#1#/*#1##(*../%&&@@@@@@@@%/********,,#1#/*,,#1#/(//******#1#///////(###%%%#//**#1#///(/*,**#1#(%&&&&&&&&%%%%############%%%%%&&&&&%%%%%%%##%##########(((((((((((((((((
%%%%%%%/...,/((##################%%#(#*#1#//*,..,..   .  . (@@@(,,,....      .*(#(/*,..,*(/*,****#1#///(&@@&&&@&%###(##(/((/*****#1#(%&&&&&&&%%%###%%%#########%%%%&&&%%%%%%%%%%%#############(((((((((((((
%%%%%/...,/((###########%%%%%%%%%%(#%#(#((##(///**,.,#1#//(*&@@%##(((*,,,,,,,,. .*##.. ..,,******#1#///////(((#(/////////((//**#1#////#&@&&&&%%%%%#%%%%%%%%%##%%%%%%%&%%%%%%%%%%%%%###########(((((((((((((
%%%/..,#1#((###%%%%%%%%%%%%%%%%%%#(#%%#(#%&#(/,(((/#1#((##((/#1#//////(%%#####(/,....,*,,,#1#((/***#1#/////////(((##(///(((##(////#1#////(#&&&&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#################(((((
%(,,,,#1#((##%%%%%%%%%%%%%%%%%%%%#(%&%##%%%###((%%###/*,,,,,,,,,,,,,,,,,,...,/(((/(#(/#1#((#(/#1#/////////((((((#&%%##%%%&%#(((((/(((((#%&&&&&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%#####################
,,,,#1#((##%%%%##%&%%%%%%%%%%%%%%####//#%%%&%%&&%(///***,,,,,,,,,,,,...,,,,,**#1#(/#1#(((((((*#1#/////////(((((((%@@&&&&&&&%##(((((((((((#&&&&&&&&&&&%%%%%%%&&%%%%%%%%%%%%%%%%%%%%%%%%%%####################
**#1#((##%%&&&&&&&&&%%%%%%%%%%%%%%%###(#(#%&&##((((///**************,,,,,,,,,,*****#1#/***#1#/((((((((((((((###%&&&&&#((%&%###(((((((((((#&&&&&&&&&&&&&&%%%%%&&%%%%%%%%%%%%%%%%%%%%%%%%%%%#################
(//(##%%%%%%&&@@@@&&&&&&&&%%%%%%%%%##%###(///(#####(((///////////////////////(((((((((((((((((((((((((((###%%&&&&%(//%%##(((((((//(((((##%%&&&&&&&&&&&&&%%%%&&&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%##########
###%%%&&&%&&&&&&%&@@@&@&&&&%&&&&&&#((#%#(/(#%%%%%%%%####((((((#####################################(((#####%&&&&&%#((%%#(((((((((((/(((((###%%%&&&&&&&&&&&&%%%&&&&&&&%%%%%%%%%%%%%%%%%%%%###############
%%%&&&&&&&&&%%&&&&&&&@@@@@&&&&@&&%((##%%%&&&&&&&&&&&&&&&&&%%%%&&&&&&&&&&&&&&%%%%%%%%%%%%%%%##############%%%&&&&&##((#%#(((//((((((((((((((####%%&&&&&&&&&&&&%%%%%%&&&&&&&%%%%%%###################(((((
%%&&&&&&@@%%%&&&&&&&&&&&&@@@@@@&&######%%&&&@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%%%%%%%%%%%%%%&&&&###((###(((//(((((((((((((##(#####%%&&&&&&&&&&&%%%%%#####%%&&%%%%%%%################(((#
&&&&&@@@@%&&&&&&&%%&&&&@@@@@@@@&%#((###%&&&@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%&&&&&&&&&&&&%%&&&&&%&&&&%####(##(((((((((((((((((((########%%%%%%&&&&&&&&&%%&&&&&%%%##%%%%%%%%%%%%###############
&@@@@@@@&&&&&&&%%&&&&@@@@@@@@@@%(((###%&&&&@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&%%%%&&&&&&&&&&&&&&&&&&&&&&&&%&&&&&%####(#((((((((((((((((((((########%%%%%%%&&&&&&&&&&&&&&&&&&%%%%%%%%%%%&&&&&%%###########
&&&&&&&%&&&&&&&@@@&&@@@@@@@@@&%(((##%%%&@@&&@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%&&&&&##%%%%##(((((((((((((###############%%%%%%&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%###%%%%%%%#######
&&&&&&%&&&&&&&&&&&@@@@@@@&&&&%(((#%&&&&&@&&&&&@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%&&&&&%%%&&&&&%(((((((####################%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%###%%%%%%%%%%
&&&@&%&&&&&&&&&&@@@@@&&%#%&&%(/(#%%&&%%&@&&&&&&&@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%##%&&&&%#(########################%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%%%%%%%%
@@@&&&&&%&&@&@@@@@@@&@&&%&%#//##%%%&&%%@@&%%%&&&&@@@@&&&@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%##%%%%%%#######################%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%%
@@&%&&&&&&&&@@@@@@&&&&@@@&#//#%%#%&&&&&@@&%%%%&&&@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%%%%%%%%%#####################%%%%%%%%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&%%%%
@&&&&&&&&&@@@@@@@&&&&&&@@#/(#%#((#&&&&&@&%%%%%%&&@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&@@@@@@@&&&%%%&%%%%%%%%%%%%#############%%%%%%%%%%%%%%%&&&&%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
&%&&&&&&&@@@@@@@@&&&&&&&#/(%%(/((#&&&&@@&%%%%%%&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&@@@@@@@@@@&&&&%%%%&%%%%%%%&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
%&&&&&&&@@@@@@@@&&&&&&&%((((//((#&&&&&@&%%%%%%%%&&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&%%%%%&&%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&&%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
&&&&&&@@@@@@@@&&&&&&@@%((###(/#%&&&&&@@&%%%%%%%%&&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&&&%%%%%&%%%%%%%&&&&&%%%%%%%%%%%%%%%%%%%%%%%&&&&&&&%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&@&&&&&&&&&&&&&&&
&&&&&@@@@@@&%&&&&&&@@&####%%((#%&&&&&@&&%%%%%%%%&&&&&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&%&&&%%%%%%&&&&&&&%%%%%%%%%%%%%%%%%%%&&&&&&&&&&%%%%%%%%%%%%&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&*/