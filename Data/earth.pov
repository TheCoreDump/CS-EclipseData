/*////////////////////////////////////////////////////////////
EARTH INCLUDE FILE

by Micha Riser 
micha@micha.virtualave.net
http://micha.virtualave.net

from the POV-Ray Objects Collection
at http://povobjects.keyspace.de
-----------------------------------

For rendering the earth with full quality you need two pictures
which you can download from the internet:
- an image map for the earth (gives the colors)
- a bump map for the earth (gives the bumps)

A good resource for these pictures is at
 http://www.jht.cjb.net/
I found there really high resolution maps of the earth. There 
are also maps for the other planets of your solar system.

Alternative adresses to look for maps:
- http://www.radcyberzine.com/xglobe/index.html#maps
- POV-Ray Web site: Links collection
-  http://povobjects.keyspace.de/cgi-bin/suche_links.cgi?X=planet
  

*/////////////////////////////////////////////////////////////





#include "colors.inc"

//////////////////////////////////////
//------------- Parameters ---------//

#declare lines_color=rgbt <0,0,0,.5>; //grid map color
#declare lines_thick=.1;      //earth's grid map: thickness of the lines (in degrees*2)
#declare lines_angle=20;      //earth's grid map: angle between lines (in degree)
#declare earth_radius=6366;   //earth sphere's radius 
#declare groundpigment=Green  //earth's pigment when no image map is chosen

#declare lines=false;         //set to true if you want to have a grid map on the earth
#declare earthbump=true;      //set to true if you want to apply a bump map to the earth
#declare earthimg=true;       //set to true if you want to apply an image map to the earth
#declare atmosphere=false;    //set to true if you want the earth sphere to have an atmosphere media around 
                              //         (not very good, needs improvement!)

//                                  //
//////////////////////////////////////

#macro make_hlines(deg_interval,line_pig,line_thick_deg)

  #local line_thick=line_thick_deg/180*pi;
  #local i=0;
  #while (i<pi/2)
	  [sin(i-line_thick) rgbft 1]
	  [sin(i-line_thick) line_pig]
		[sin(i+line_thick) line_pig]
	  [sin(i+line_thick) rgbft 1] 
    #local i=i+deg_interval/180*pi;
  #end

#end

#declare piglines_h=pigment{
  gradient y
	pigment_map{	  
	  make_hlines(lines_angle,lines_color,lines_thick)
	  }
		scale earth_radius
	}	 

#declare piglines_v=pigment{
  radial
	pigment_map{
	  [0 rgbft 1]
		[0 lines_color]
		[lines_thick/lines_angle*3 lines_color]
		[lines_thick/lines_angle*3 rgbft 1]
		}
	frequency 360/lines_angle	
	}


#if (earthimg=true)

#declare pigmap1=
pigment{
	  average
		
		pigment_map{
		[1
	  image_map{
       png "earth.png"   //path to the IMAGE MAP file
			 map_type 1
			 interpolate 4
		   }
	  scale earth_radius
	    ]
    }
   }

#end

#if (earthbump=true)

#declare earthbumps= 
  normal{
    bump_map{
		  tga
			"EarthBumpBIG.tga"  //path to the BUMP MAP file
			map_type 	1		
			bump_size 7
			}
	  scale earth_radius
    }
		
#end		
		

#declare earth=union{

sphere{0,earth_radius

texture{
  #if (earthimg=true)
 	pigment{pigmap1}	
	#else
	pigment{groundpigment}
	#end
	#if (earthbump=true)
	normal{earthbumps}
	#end
	 }
#if (lines=true)	 
texture{pigment{ piglines_v}}
texture{pigment{ piglines_h}}
#end	
		
		
	
}

#if (atmosphere=true)

difference{
sphere{0,earth_radius+100}
sphere{0,earth_radius+0.01}

hollow
pigment{rgbf 1}

interior{
 media{
  scattering{2,<0.0005,0.0005,0.001>*.5}
  intervals 5
  }

 }

}

#end

}

////////////////////////////////////////////////////////

//uncomment following for a test render
/*

camera{

location<0,0,-40000>
look_at 0

angle 30
}

light_source{
<1000,100,-100000>
color White*1.4

}

background{rgb .05}

object{earth rotate y*-130 rotate x*-20}
*/

////////////////////////////////////////////////////////
