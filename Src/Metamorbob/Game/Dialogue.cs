// Type: GameManager.Dialogue


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace GameManager
{
  internal class Dialogue : Text
  {
    private string EndTextSymbol = "->";
    public Flip Flip = new Flip();
    private Text EndText;
    private const float InterruptSpeedApparition = 0.001f;
    private float BaseSpeedApparition;
    public List<Keys> KeyToSwitch = new List<Keys>();
    public List<Buttons> ButtonToSwitch = new List<Buttons>();
    public OnComplete OnComplete;

    private string[] FullText { get; set; }

    private int CurrentTextPosition { get; set; }

    public bool ForceTextToFinish { get; set; }

    public Dialogue(
      SpriteFont pFont,
      string[] pString,
      Vector2 pos,
      Color pColor,
      Text.TextMode tMode = Text.TextMode.LETTER_APPARITION,
      int pWidth = 0,
      float pAlpha = 1f)
      : base(pFont, pString[0], pos, pColor, tMode, pWidth, pAlpha)
    {
      this.FullText = pString;
      this.CurrentTextPosition = 0;
      this.KeyToSwitch.Add((Keys) 13);
      this.KeyToSwitch.Add((Keys) 88);
      this.KeyToSwitch.Add((Keys) 32);
      this.ButtonToSwitch.Add((Buttons) 4096);
      this.ButtonToSwitch.Add((Buttons) 8192);
      this.ButtonToSwitch.Add((Buttons) 16384);
      this.EndText = new Text(this.Font, this.EndTextSymbol, new Vector2(pos.X + this.Width, (float) ((double) pos.Y + (double) this.Height + 10.0)), this.Color);
      this.EndText.CanBlink = true;
      this.EndText.timerVisible = 0.25f;
      this.EndText.BlinkFrequency = 0.25f;
      this.EndText.IsActive = false;
      this.BaseSpeedApparition = this.SpeedApparition;
    }

    public void Reset()
    {
      this.CurrentTextPosition = 0;
      this.EndText.IsActive = false;
      this.CurrentString = "";
      this.currentString_position = 0;
      this.FullString = this.FullText[0];
      this.IsFullStringAppear = false;
      this.BaseSpeedApparition = this.SpeedApparition;
    }

    public void Reset(string[] NewText)
    {
      this.CurrentTextPosition = 0;
      this.EndText.IsActive = false;
      this.CurrentString = "";
      this.currentString_position = 0;
      this.FullText = NewText;
      this.FullString = this.FullText[0];
      this.IsFullStringAppear = false;
      this.BaseSpeedApparition = this.SpeedApparition;
    }

    public override void Update(GameTime gameTime)
    {
      this.Width = this.Font.MeasureString(this.CurrentString).X * this.Scale.X;
      this.Height = this.Font.MeasureString(this.CurrentString).Y * this.Scale.Y;
      if ((double) this.SpeedApparition != 1.0 / 1000.0)
        this.BaseSpeedApparition = this.SpeedApparition;
      this.EndText.Position = new Vector2(this.Position.X + this.Width, (float) ((double) this.Position.Y + (double) this.Height + 10.0));
      switch (this.Align)
      {
        case Util.Alignement.CENTER_X:
          this.Position = new Vector2(Camera.Position.X + ((float) (Camera.VisibleArea.Width / 2) - this.Width / 2f), this.Position.Y);
          break;
        case Util.Alignement.CENTER_Y:
          this.Position = new Vector2(this.Position.X, Camera.Position.Y + ((float) (Camera.VisibleArea.Height / 2) - this.Height / 2f));
          break;
        case Util.Alignement.CENTER_X | Util.Alignement.CENTER_Y:
          this.Position = new Vector2(Camera.Position.X + ((float) (Camera.VisibleArea.Width / 2) - this.Width / 2f), this.Position.Y);
          this.Position = new Vector2(this.Position.X, Camera.Position.Y + ((float) (Camera.VisibleArea.Height / 2) - this.Height / 2f));
          break;
      }
      if (this.IsActive)
      {
        if (this.Mode == Text.TextMode.LETTER_APPARITION)
        {
          this.currentTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
          if (this.currentString_position < this.FullText[this.CurrentTextPosition].Length)
          {
            this.EndText.IsActive = false;
            this.currentTimer -= 0.01f;
            if ((double) this.currentTimer < 0.0)
            {
              if (this.FieldWidth != 0 && (double) this.Font.MeasureString(this.MidString).X >= (double) this.FieldWidth)
              {
                this.CurrentString += "\n";
                this.MidString = "";
                if (char.ToString(this.FullString[this.currentString_position]) == " ")
                {
                  ++this.currentString_position;
                }
                else
                {
                  int num = 0;
                  for (; char.ToString(this.FullString[this.currentString_position]) != " "; --this.currentString_position)
                  {
                    this.CurrentString.Remove(this.currentString_position - 1);
                    ++num;
                  }
                  this.currentString_position += num - 1;
                  this.canAddNextLetter = false;
                }
              }
              if (this.canAddNextLetter)
              {
                this.CurrentString += this.FullString[this.currentString_position].ToString();
                this.MidString += this.FullString[this.currentString_position].ToString();
              }
              else
                this.canAddNextLetter = true;
              if (this.FullString[this.currentString_position] != ' ' && this.Sound != null)
                this.Sound.Play(Game1.VOLUME_SFX, this.TextSoundPitch, 0.0f);
              ++this.currentString_position;
              this.currentTimer = this.SpeedApparition;
            }
            foreach (Keys key in this.KeyToSwitch)
            {
              if (KBInput.JustPressed(key))
                this.SpeedApparition = 1f / 1000f;
            }
            foreach (Buttons button in this.ButtonToSwitch)
            {
              if (GamePadInput.JustPressed(button))
                this.SpeedApparition = 1f / 1000f;
            }
          }
          else
          {
            this.EndText.IsActive = true;
            if (this.CurrentTextPosition != this.FullText.Length - 1)
            {
              foreach (Keys key in this.KeyToSwitch)
              {
                if (KBInput.JustPressed(key))
                {
                  ++this.CurrentTextPosition;
                  this.MidString = "";
                  this.CurrentString = "";
                  this.FullString = this.FullText[this.CurrentTextPosition];
                  this.currentString_position = 0;
                  this.SpeedApparition = this.BaseSpeedApparition;
                }
              }
              foreach (Buttons button in this.ButtonToSwitch)
              {
                if (GamePadInput.JustPressed(button) 
                   //|| TouchInput.JustLeftClicked()
                )
                {
                  ++this.CurrentTextPosition;
                  this.MidString = "";
                  this.CurrentString = "";
                  this.FullString = this.FullText[this.CurrentTextPosition];
                  this.currentString_position = 0;
                  this.SpeedApparition = this.BaseSpeedApparition;
                }
              }
            }
            else
            {
              foreach (Keys key in this.KeyToSwitch)
              {
                if (KBInput.JustPressed(key))
                  this.IsFullStringAppear = true;
              }
              foreach (Buttons button in this.ButtonToSwitch)
              {
                if 
                (GamePadInput.JustPressed(button) 
                   // || TouchInput.JustLeftClicked()
                )
                  this.IsFullStringAppear = true;
              }
              if (this.OnComplete != null)
                this.OnComplete();
              if (this.IsFullStringAppear)
                this.ToRemove = true;
            }
          }
        }
        if (this.ForceTextToFinish)
        {
          this.IsFullStringAppear = true;
          this.IsActive = false;
          this.ToRemove = true;
        }
        if (this.CanBlink)
        {
          this.timerVisible -= (float) gameTime.ElapsedGameTime.TotalSeconds;
          if ((double) this.timerVisible < 0.0)
          {
            if (this.IsVisible)
              this.timerVisible = this.BlinkFrequency / 2f;
            else
              this.timerVisible = this.BlinkFrequency;
            this.IsVisible = !this.IsVisible;
          }
        }
      }
      this.EndText.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (!this.IsActive || !this.IsVisible)
        return;
      this.flipEffect = (SpriteEffects) 0;
      if (this.Flip.X)
        this.flipEffect = (SpriteEffects) 1;
      if (this.Flip.Y)
        this.flipEffect = (SpriteEffects) 2;
      if (this.Flip.X && this.Flip.Y)
        this.flipEffect = (SpriteEffects) 3;
      spriteBatch.DrawString(this.Font, this.CurrentString, this.Position, Color.Multiply(this.Color, this.Alpha), MathHelper.ToRadians(this.Angle + Camera.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      this.EndText.Draw(spriteBatch);
    }
  }
}
