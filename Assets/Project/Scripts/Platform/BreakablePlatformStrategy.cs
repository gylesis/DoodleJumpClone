using UniRx;

namespace Project.Platform
{
    public class BreakablePlatformStrategy : IPlatformStrategy
    {
        public void Process(Platform platform)
        {
            platform.Rigidbody.isKinematic = false;
            platform.PlatformJumped.Subscribe((OnPlatformJumped));
        }

        private void OnPlatformJumped(Platform platform)
        {
            platform.PlatformView.PlayBreakAnimation();
            platform.Rigidbody.gravityScale = 3;
            platform.Collider.enabled = false;
            platform.ToRecycle.OnNext(platform);
        }
    }
}