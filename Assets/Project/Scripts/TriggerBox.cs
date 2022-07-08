using UniRx;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerBox : MonoBehaviour
    {
        public Subject<TriggerEventContext> Triggered { get; } = new Subject<TriggerEventContext>();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var triggerEventContext = new TriggerEventContext();
            triggerEventContext.Other = other;
            triggerEventContext.Sender = this;

            Triggered.OnNext(triggerEventContext);
        }
    }
}