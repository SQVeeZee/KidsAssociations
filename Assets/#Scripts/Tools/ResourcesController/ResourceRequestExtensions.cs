using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ResourceRequestExtensions
{
    public static ResourceRequestAwaiter GetAwaiter(this ResourceRequest request) => new ResourceRequestAwaiter(request);

    public struct ResourceRequestAwaiter : INotifyCompletion
    {
        private readonly ResourceRequest request;

        public ResourceRequestAwaiter(ResourceRequest request) => this.request = request;
        public ResourceRequestAwaiter GetAwaiter() => this;
        public UnityEngine.Object GetResult() => request.asset;
        public bool IsCompleted => request.isDone;
        public void OnCompleted(Action action) => request.completed += _ => action();
    }
}
