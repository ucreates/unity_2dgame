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
            animation = this.owner.GetComponent<SkeletonAnimation>();
        }

        public SkeletonAnimation animation { get; }

        public void Play(string animationName, bool loop = true)
        {
            animation.state.SetAnimation(0, animationName, loop);
        }

        public void Play(string animationName, AnimationState.TrackEntryDelegate callback, bool loop = true)
        {
            if (null != callback) animation.state.Complete += callback;
            animation.state.SetAnimation(0, animationName, loop);
        }
    }
}