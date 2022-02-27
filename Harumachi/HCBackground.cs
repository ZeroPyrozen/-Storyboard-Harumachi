using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class HCBackground : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var bgLayer = GetLayer("Background");
            var titleLayer = GetLayer("Title");
            var bg = bgLayer.CreateSprite("sb/darkbg.png");
            var title = titleLayer.CreateSprite("sb/title.png",OsbOrigin.BottomCentre, new Vector2(320,240));
            var bgPush = bgLayer.CreateSprite("sb/darkbg.png",OsbOrigin.Centre, new Vector2(320,300));
            bg.Fade(-322,1);
            bg.Fade(14191,0);
            title.Fade(28926,29455,0,1);
            title.Fade(32632,36882,1,0);
            title.Scale(0,0.3);
            bgPush.Scale(13926,480.0/1080.0);
            bgPush.Color(13926,new Color4(239,133,140,0));
            bgPush.Fade(13926,1);
            bgPush.Fade(36882,0);
            bgPush.MoveY(OsbEasing.OutExpo,13926,14191,360,240);
            bgPush.Color(OsbEasing.In,28661,28838,bgPush.ColorAt(28661),Color4.White);
        }
    }
}
