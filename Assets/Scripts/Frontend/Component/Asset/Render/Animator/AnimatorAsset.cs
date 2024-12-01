using Spine;
using Spine.Unity;

namespace Frontend.Component.Asset.Render
{
    public sealed class AnimatorAsset : BaseRenderAsset
    {
        public AnimatorAsset()
        {
        }

        public AnimatorAsset(BaseBehaviour owner)
        {
            this.owner = owner;
            anime = this.owner.GetComponent<SkeletonAnimation>();
        }

        public SkeletonAnimation anime { get; }

        public void Play(string animationName, bool loop = true)
        {
            anime.state.SetAnimation(0, animationName, loop);
        }

        public void Play(string animationName, AnimationState.TrackEntryDelegate callback, bool loop = true)
        {
            if (null != callback) anime.state.Complete += callback;
            anime.state.SetAnimation(0, animationName, loop);
        }
    }
}