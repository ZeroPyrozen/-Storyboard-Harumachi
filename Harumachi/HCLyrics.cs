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
    public class HCLyrics : StoryboardObjectGenerator
    {
        FontGenerator fg;
        [Configurable]
        public int FadeDuration = 250;

        public override void Generate()
        {
            fg = LoadFont("sb/en", new FontDescription()
            {
                FontPath = "AGGLT.otf",
                FontSize = 128,
                Color = Color4.White,
                Padding = Vector2.Zero,
                FontStyle = System.Drawing.FontStyle.Bold,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false
            });

            //General Lyrics
            GenerateLine("1",-322,161, new Color4(92,194,207,0),0.5, customFadeDuration:88);
            GenerateLine("2",867,1132, new Color4(92,194,207,0),0.5, customFadeDuration:88);
            GenerateLine("1",1485,1926, new Color4(92,194,207,0),0.5, customFadeDuration:88);
            GenerateLine("2",1749,2101, new Color4(92,194,207,0),0.5, customFadeDuration:88);
            GenerateLine("Me no mae no `tobira ``o ~ake``tara `haru`kaze",2102,5455,new Color4(83,68,69,0),customScale:0.1,customPositionY:280);
            GenerateLine("Tori-`tachi mo kigi de ``machi``awase",5691,8279,new Color4(83,68,69,0),customScale:0.1,customPositionY:280);
            GenerateLine("Kimi e mukau ``shingoo ```wa aozora-````iro",8514,12514,new Color4(83,68,69,0),customScale:0.1,customPositionY:280);
            GenerateLine("Kakedaseba ii",12749,14083,new Color4(83,68,69,0),customScale:0.1,customPositionY:280);
            GenerateLine("Usotsuki `kaku`ritsu-```ron ``toka",14279,16593,Color4.White,customScale:0.1,customPositionY:280,isKiai:true);
            GenerateLine("Ichi purasu `ichi ga mugen ``toka",17102,19416,Color4.White,customScale:0.1,customPositionY:280,isKiai:true);
            GenerateLine("Oshiete kure``ta `kimi ``to sagashi ``ni yukou",19573,23808,Color4.White,customScale:0.1,customPositionY:280,isKiai:true);
            GenerateLine("Harumachi ~Clover",23965,26475, Color4.White,0.4,customPositionY:320,isKiai:false);   

            GenerateLine("目`の``前`の`扉```を`~あけ``たら``春`風",2102,5455,new Color4(83,68,69,0));
            GenerateLine("鳥`たち`も`木々`で```待ち``合わせ",5691,8279,new Color4(83,68,69,0));
            GenerateLine("君`へ`向かう```信号````は`青空````色",8514,12514,new Color4(83,68,69,0));
            GenerateLine("駆け出せば`いい",12749,14083,new Color4(83,68,69,0));
            GenerateLine("ウソつき``確`率```論```とか",14279,16593,Color4.White,isKiai:true);
            GenerateLine("1 + `1 が ∞ ``とか",17102,19416,Color4.White,isKiai:true);
            GenerateLine("教えて`くれ``た``君```と`探し```に`行こう",19573,23808,Color4.White,isKiai:true);
            GenerateLine("春待ちク`~ローバー",23965,26475, Color4.White,0.4,isKiai:false);    

            //Custom Lyrics
            GenerateOhNo(26632,28132,-1);
            GenerateOhNo(27779,28661,1);    
        }

        void GenerateOhNo(int startTime, int endTime, int bottom)
        {
            var lyricLayer = GetLayer("Lyric");
            string text = "Oh ~no!";
            //Generate the first one
            double scale = 0.5;
            double afterScale = 1;
            double lineWidth = 0f;
            Color4 color = Color4.White;
            double lineWidthAfterScale = 0f;
            foreach (var letter in text)
            {
                if(letter == '`' || letter == '~')
                    continue;
                var texture = fg.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
                lineWidthAfterScale += texture.BaseWidth *afterScale;
            }
            double posX = 0;
            double posXAfterScale = 0;
            double currTime = startTime;
            var beatDuration = Beatmap.GetTimingPointAt(startTime).BeatDuration;
            foreach (var letter in text)
            {
                var texture = fg.GetTexture(letter.ToString());
                
                if(letter == ' ' || letter == '`'){
                    currTime += beatDuration/2;
                }
                else if(letter == '~')
                {
                    currTime -= beatDuration/2;
                }
                if(letter == '`' || letter == '~')
                    continue;
                if(!texture.IsEmpty)
                {
                    var Origin = OsbOrigin.BottomLeft;
                    var yPos = 240+(bottom)*(texture.BaseHeight/2);
                    var easing = OsbEasing.OutBack;
                    var x = 320-0.5*lineWidth + posX;
                    var xS = 320-0.5*lineWidthAfterScale + posXAfterScale;
                    var sprite = lyricLayer.CreateSprite(
                        texture.Path, 
                        Origin, 
                        new Vector2((float)(x),yPos)
                    );
                    if(letter == 'g' || letter == 'p'|| letter == 'y')
                    {
                        yPos = yPos + (int)((texture.Height-fg.GetTexture("a").Height) * scale);
                    }
                    else if(letter == '-'||letter == 'ー'||letter == '∞'||letter == '+')
                    {
                        yPos = yPos + (int)((texture.Height-fg.GetTexture("a").Height)/2 * scale);
                    }
                    sprite.MoveY(easing,currTime, currTime+beatDuration,yPos+20,yPos);
                    //sprite.Scale(startTime, scale);
                    sprite.Scale(easing,currTime,currTime+beatDuration/2,scale,afterScale);
                    sprite.MoveX(easing,currTime,currTime+beatDuration/2,x,xS);
                    sprite.Scale(easing,currTime+beatDuration/2,currTime+beatDuration*1.5,afterScale,scale);
                    sprite.MoveX(easing,currTime+beatDuration/2,currTime+beatDuration*1.5,xS,x);
                    sprite.Fade(currTime,currTime+beatDuration,0,1);
                    sprite.Color(startTime,color);
                    sprite.Fade(endTime,endTime+beatDuration,1,0);
                }
                posX += texture.BaseWidth * scale;
                posXAfterScale += texture.BaseWidth * afterScale;
            }
        }

        void GenerateLine(string text, int startTime, int endTime, Color4 color, double customScale=0.3, int customPositionY=240, bool isKiai=false, int customFadeDuration=0)
        {
            var lyricLayer = GetLayer("Lyric");
            double scale = customScale;
            double lineWidth = 0f;
            int fadeDuration = 0;
            if(customFadeDuration!=0)
                fadeDuration = customFadeDuration;
            else
                fadeDuration = FadeDuration;
            foreach (var letter in text)
            {
                if(letter == '`' || letter == '~')
                    continue;
                var texture = fg.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }
            double posX = 0;
            double currTime = startTime;
            var beatDuration = Beatmap.GetTimingPointAt(startTime).BeatDuration;
            Vector2 randomPosition = Vector2.Zero;
            
            foreach (var letter in text)
            {
                var texture = fg.GetTexture(letter.ToString());
                //Handle Lyrics Speed
                if(letter == ' ' || letter == '`')
                {
                    currTime += beatDuration/2;
                }
                else if(letter == '~')
                {
                    currTime -= beatDuration/2;
                }
                if(letter == '`' || letter == '~')
                    continue;

                if(!texture.IsEmpty)
                {
                    var Origin = OsbOrigin.BottomLeft;
                    var yPos = customPositionY;
                    var easing = (isKiai)?OsbEasing.InOutSine:OsbEasing.None;
                    var sprite = lyricLayer.CreateSprite(
                        texture.Path, 
                        Origin, 
                        new Vector2((float)(320-0.5*lineWidth + posX),yPos)
                    );
                    randomPosition = GenerateRandomPoint(50, sprite.PositionAt(currTime));
                    var newRandomYPos = randomPosition.Y;
                    if(letter == ' ')
                    {
                        randomPosition = GenerateRandomPoint(50, sprite.PositionAt(currTime));
                        if(letter == 'g' || letter == 'p'|| letter == 'y')
                        {
                            randomPosition.Y = randomPosition.Y + (int)((texture.Height-fg.GetTexture("a").Height) * scale);
                        }
                        else if(letter == '-'||letter == 'ー'||letter == '∞'||letter == '+')
                        {
                            randomPosition.Y = randomPosition.Y + (int)((texture.Height-fg.GetTexture("a").Height)/2 * scale);
                        }
                    }
                    if(letter == 'g' || letter == 'p'|| letter == 'y')
                    {
                        yPos = yPos + (int)((texture.Height-fg.GetTexture("a").Height) * scale);
                        newRandomYPos = randomPosition.Y + (int)((texture.Height-fg.GetTexture("a").Height) * scale);
                    }
                    else if(letter == '-'||letter == 'ー'||letter == '∞'||letter == '+')
                    {
                        yPos = yPos + (int)((texture.Height-fg.GetTexture("a").Height)/2 * scale);
                        newRandomYPos = randomPosition.Y + (int)((texture.Height-fg.GetTexture("a").Height)/2 * scale);
                    }
                    if(!isKiai)
                        sprite.MoveY(easing,currTime, currTime+beatDuration,yPos+20,yPos);

                    sprite.Scale(startTime, scale);
                    sprite.Fade(currTime,currTime+beatDuration,0,1);
                    if(isKiai)
                    {
                        sprite.Move(OsbEasing.OutBack,currTime, currTime+beatDuration, new Vector2(sprite.PositionAt(currTime).X, yPos), new Vector2(randomPosition.X, newRandomYPos));
                        sprite.Move(OsbEasing.OutBack,currTime+beatDuration+1, currTime+beatDuration+100, sprite.PositionAt(currTime+beatDuration+1), new Vector2(sprite.PositionAt(currTime).X, yPos));
                    }
                    sprite.Color(startTime,color);
                    sprite.Fade(endTime,endTime+fadeDuration,1,0);
                }
                posX += texture.BaseWidth * scale;
            }
        }

        Vector2 GenerateRandomPoint(int radius, Vector2 currentPosition)
        {
            Random rng =  new Random();
            var θ = rng.NextDouble();
            return new Vector2((float)(radius * Math.Cos(θ) + currentPosition.X), 
                    (float)(radius * Math.Sin(θ) + currentPosition.Y));
        }
    }
}
