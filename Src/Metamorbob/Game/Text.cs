
// Type: GameManager.Text




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace GameManager
{
  internal class Text : IActor
  {
    protected string MidString = "";
    public Vector2 Origin;
    public Vector2 Scale;
    public float Angle;
    public Flip Flip;
    protected SpriteEffects flipEffect;
    public float SpeedApparition = 0.1f;
    protected float currentTimer;
    protected int currentString_position;
    public float timerVisible = 0.5f;
    public float BlinkFrequency = 0.5f;
    public bool CanBlink;
    public bool IsFullStringAppear;
    protected bool canAddNextLetter = true;

    public Vector2 Position { get; set; }

    public bool ToRemove { get; set; }

    public SpriteFont Font { get; private set; }

    public float Width { get; protected set; }

    public float Height { get; protected set; }

    public Util.Alignement Align { get; set; }

    public SoundEffect Sound { get; set; }

    public float TextSoundPitch { get; set; }

    public string FullString { get; set; }

    public string CurrentString { get; set; }

    public int FieldWidth { get; set; }

    public Color Color { get; set; }

    public float Alpha { get; set; }

    public bool IsVisible { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public bool EffectBold { get; set; }

    public bool EffectFake3D { get; set; }

    public Color ColorFake3D { get; set; } = Color.White;

    public Rectangle BoundingBox { get; set; }

    public Text.TextMode Mode { get; private set; }

    public Text(
      SpriteFont pFont,
      string pString,
      Vector2 pos,
      Color pColor,
      Text.TextMode tMode = Text.TextMode.NORMAL,
      int pWidth = 0,
      float pAlpha = 1f)
    {
      this.CurrentString = "";
      this.FullString = pString;
      this.Position = pos;
      this.Color = pColor;
      this.Font = pFont;
      this.Alpha = pAlpha;
      this.Mode = tMode;
      this.FieldWidth = pWidth;
      if (this.Mode == Text.TextMode.NORMAL)
      {
        this.CurrentString = this.FullString;
        this.IsFullStringAppear = true;
      }
      this.currentTimer = this.SpeedApparition;
      this.Flip.X = false;
      this.Flip.Y = false;
      this.Scale = Vector2.One;
      this.Origin = Vector2.Zero;
      this.Width = this.Font.MeasureString(this.FullString).X * this.Scale.X;
      this.Height = this.Font.MeasureString(this.FullString).Y * this.Scale.Y;
      this.Align = Util.Alignement.NONE;
    }

    public virtual void TouchedBy(IActor By)
    {
    }

    public bool IsOnScreen() => Util.Overlaps((IActor) this, Camera.VisibleArea);

    public void Reset(string newString, Text.TextMode? newMode = null)
    {
      this.FullString = newString;
      if (!newMode.HasValue)
      {
        this.CurrentString = this.Mode == Text.TextMode.LETTER_APPARITION ? "" : this.FullString;
      }
      else
      {
        Text.TextMode? nullable = newMode;
        Text.TextMode textMode = Text.TextMode.LETTER_APPARITION;
        this.CurrentString = nullable.GetValueOrDefault() == textMode & nullable.HasValue ? "" : this.FullString;
      }
      this.currentString_position = 0;
      this.IsFullStringAppear = false;
    }

    public virtual void Update(GameTime gameTime)
    {
      this.Width = this.Font.MeasureString(this.FullString).X * this.Scale.X;
      this.Height = this.Font.MeasureString(this.FullString).Y * this.Scale.Y;
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
      if (!this.IsActive)
        return;
      if (this.Mode == Text.TextMode.LETTER_APPARITION)
      {
        this.currentTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        if (this.currentString_position < this.FullString.Length)
        {
          this.currentTimer -= 0.01f;
          if ((double) this.currentTimer < 0.0)
          {
            if (this.FieldWidth != 0 && (double) this.Font.MeasureString(this.CurrentString).X >= (double) this.FieldWidth)
              this.CurrentString += "\n";
            this.CurrentString += this.FullString[this.currentString_position].ToString();
            if (this.FullString[this.currentString_position] != ' ' && this.Sound != null)
              this.Sound.Play(Game1.VOLUME_SFX, this.TextSoundPitch, 0.0f);
            ++this.currentString_position;
            this.currentTimer = this.SpeedApparition;
          }
        }
        else
          this.IsFullStringAppear = true;
      }
      else
        this.FullString = this.CurrentString;
      if (!this.CanBlink)
        return;
      this.timerVisible -= (float) gameTime.ElapsedGameTime.TotalSeconds;
      if ((double) this.timerVisible >= 0.0)
        return;
      this.timerVisible = !this.IsVisible ? this.BlinkFrequency : this.BlinkFrequency / 2f;
      this.IsVisible = !this.IsVisible;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
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
      if (this.EffectFake3D)
        spriteBatch.DrawString(this.Font, this.CurrentString, new Vector2((float) ((double) this.Position.X - (double) this.Scale.X - 1.0), (float) ((double) this.Position.Y - (double) this.Scale.Y - 1.0)), Color.Multiply(this.ColorFake3D, this.Alpha), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      spriteBatch.DrawString(this.Font, this.CurrentString, this.Position, Color.Multiply(this.Color, this.Alpha), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
      if (!this.EffectBold)
        return;
      spriteBatch.DrawString(this.Font, this.CurrentString, new Vector2(this.Position.X + this.Scale.X, this.Position.Y), Color.Multiply(this.Color, this.Alpha), MathHelper.ToRadians(this.Angle), this.Origin, this.Scale, this.flipEffect, 0.0f);
    }

    public enum TextMode : byte
    {
      NORMAL,
      LETTER_APPARITION,
    }
  }
}
