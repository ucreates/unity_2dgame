using Frontend.Behaviour.Base;
using Spine.Unity;
namespace Frontend.Component.Asset.Render
{
    public sealed class AnimatorAsset : BaseRenderAsset {
    public SkeletonAnimation anime {
        get;
        private set;
    }
    public AnimatorAsset() {
    }
    public AnimatorAsset(BaseBehaviour owner) {
        this.owner = owner;
        this.anime = this.owner.GetComponent<SkeletonAnimation>();
    }
    public void Play(string animationName, bool loop = true) {
        this.anime.state.SetAnimation(0, animationName, loop);
    }
    public void Play(string animationName, Spine.AnimationState.TrackEntryDelegate callback, bool loop = true) {
        if (null != callback) {
            this.anime.state.Complete += callback;
        }
        this.anime.state.SetAnimation(0, animationName, loop);
    }
}
}
