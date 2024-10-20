using NetEXT.Animation;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.VisualEffects;

internal class ParryIndicatorEffectActor : Actor
{
    private Animator<Sprite, string> _animator;
    
    public ParryIndicatorEffectActor() : base()
    {
        _animator = new Animator<Sprite, string>();
        sprite = new Sprite(AssetLoader.GetInstance().GetTexture("parry_effect"));

        FadeAnimation<Sprite> fadeOutAnim = new FadeAnimation<Sprite>(0.1f, 0.9f);
        _animator.AddAnimation("fadeOut", fadeOutAnim, Time.FromSeconds(2f));
    }

    public override void Start()
    {
        MoveToLocationOverTime(actorLocation - new Vector2f(0, 10f), 3f);
        _animator.PlayAnimation("fadeOut");
        base.Start();
    }

    public override void Tick()
    {
        base.Tick();
        _animator.Update(Time.FromSeconds(Game.GetInstance().DeltaTime));
        if (moving)
            return;
        
        _animator.StopAnimation("fadeOut");
        parentScene.DeleteActor(this);
    }

    public override void Draw()
    {
        _animator.Animate(sprite);
        base.Draw();
    }
}