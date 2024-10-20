using NetEXT.MathFunctions;
using NetEXT.Particles;
using PAS.Engine;
using SFML.Graphics;
using SFML.System;

namespace PAS.Content.VisualEffects;

internal class ConfettiParticleEffect : Actor
{
    private ParticleSystem _particleSystem;
    private UniversalEmitter _emitter;
    
    public ConfettiParticleEffect() : base()
    {
        _particleSystem = new ParticleSystem(AssetLoader.GetInstance().GetTexture("confetti"));

        _particleSystem.AddTextureRect(new IntRect(0, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(4, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(8, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(12, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(16, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(20, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(24, 0, 4, 4));
        _particleSystem.AddTextureRect(new IntRect(28, 0, 4, 4));
        
        _emitter = new UniversalEmitter();
        _emitter.ParticleTextureIndex =new Distribution<int>(ParticleTextureDistFunc);
        _emitter.EmissionRate = 10f;
        _emitter.ParticleLifetime = new Distribution<Time>(ParticleLifetimeDistFunc);
        _emitter.ParticlePosition = new Distribution<Vector2f>(ParticlePositionDistFunc);
        _emitter.ParticleVelocity = new Distribution<Vector2f>(ParticleVelocityDistFunc);
        
        _particleSystem.AddEmitter(_emitter);
    }

    private int ParticleTextureDistFunc()
    {
        return Game.GetInstance().Rand.Next(0, 7);
    }
    private Time ParticleLifetimeDistFunc()
    {
        return Time.FromSeconds(Game.GetInstance().Rand.Next(30, 60));
    }

    private Vector2f ParticlePositionDistFunc()
    {
        return new Vector2f((float)(Game.GetInstance().Rand.NextDouble() * 192), -4f);
    }
    private Vector2f ParticleVelocityDistFunc()
    {
        return new Vector2f(0, (float)(2 + Game.GetInstance().Rand.NextDouble() * (5-2)));
    }

    public override void Tick()
    {
        _particleSystem.Update(Time.FromSeconds(Game.GetInstance().DeltaTime));
        _emitter.EmitParticles(_particleSystem, Time.FromSeconds(Game.GetInstance().DeltaTime));
        base.Tick();
    }

    public override void Draw()
    {
        if(_particleSystem != null)
            Game.GetInstance().GetWindow().Draw(_particleSystem);
    }
}