
// Type: GameManager.SwitchParticle




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameManager
{
  internal class SwitchParticle : Sprite
  {
    private const byte ANGLE_SPEED = 4;
    private readonly float MIN_ALPHA_SPEED = 0.02f;
    private readonly float MAX_ALPHA_SPEED = 0.1f;
    private const byte MIN_SPEED = 1;
    private readonly byte MAX_SPEED = 5;
    private const byte MIN_SIZE = 2;
    private const byte MAX_SIZE = 8;

    public SwitchParticle(bool slowSpeed = false)
      : base(Primitive.CreatePixel())
    {
      if (slowSpeed)
        this.MAX_SPEED = (byte) 2;
      this.Drag = new Vector2(Util.RandomFloat(-(float) this.MAX_SPEED, (float) this.MAX_SPEED), Util.RandomFloat(-(float) this.MAX_SPEED, (float) this.MAX_SPEED));
      this.Velocity = this.Drag;
      this.Scale = new Vector2(Util.RandomFloat(2f, 8f));
    }

    public override void Update(GameTime gameTime)
    {
      this.Alpha -= Util.RandomFloat(this.MIN_ALPHA_SPEED, this.MAX_ALPHA_SPEED);
      if ((double) this.Alpha <= 0.10000000149011612)
        this.ToRemove = true;
      base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
