
using System;

namespace Reoria.Engine.Base.Common;

/// <summary>
/// Provides a mechanism for releasing managed and unmanaged resources using the dispose pattern.
/// Implements both synchronous and asynchronous disposal patterns to allow proper cleanup of resources.
/// </summary>
public abstract class Disposable : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Indicates whether the object has been disposed.
    /// </summary>
    protected bool isDisposed { get; private set; } = false;
    /// <summary>
    /// Gets a lock object that is used for synchronization during resource disposal.
    /// This lock ensures that disposal operations are thread-safe, preventing 
    /// multiple threads from attempting to dispose of the resource simultaneously.
    /// </summary>
    /// <remarks>
    /// The <see cref="@lock"/> is a protected property, meaning it can be accessed 
    /// within the class and its derived classes, but not outside of them. The lock object 
    /// is initialized by default to a new instance of the <see cref="Lock"/> class, which 
    /// provides the necessary mechanisms for thread synchronization.
    /// </remarks>
    protected Lock @lock { get; private set; } = new();

    /// <summary>
    /// Finalizer (destructor) for the class. It is called when the object is about to be garbage collected.
    /// It ensures resources are cleaned up if Dispose is not called manually.
    /// </summary>
    ~Disposable()
    {
        // Ensure thread-safe disposal by acquiring the lock.
        lock (this.@lock)
        {
            // Check to see if the object has been disposed or not.
            if (!this.isDisposed)
            {
                // Call the asynchronous dispose function, and don't free managed resources as the GC will handle those.
                this.DisposeAsync(disposing: false).GetAwaiter().GetResult();

                // Call the dispose function, and don't free managed resources as the GC will handle those.
                this.Dispose(disposing: false);

                // Mark the object as disposed.
                this.isDisposed = true;
            }
        }
    }

    /// <summary>
    /// Synchronously disposes the object and releases its resources.
    /// </summary>
    public void Dispose()
    {
        // Ensure thread-safe disposal by acquiring the lock.
        lock (this.@lock)
        {
            // Check to see if the object has been disposed or not.
            if (!this.isDisposed)
            {
                // Call the dispose function, and free managed resources manually.
                this.Dispose(disposing: true);

                // Mark the object as disposed.
                this.isDisposed = true;

                // The object has been disposed already, tell the GC we don't need to call the finalizer.
                GC.SuppressFinalize(this);
            }
        }
    }
    /// <summary>
    /// Synchronously disposes resources of the object.
    /// This is called by <see cref="Dispose()"/> to release managed and unmanaged resources synchronously.
    /// </summary>
    /// <param name="disposing">Indicates whether the method is being called from the <see cref="Dispose()"/> method (true) or the finalizer (false).</param>
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if the object has been disposed or not.
        if (!this.isDisposed)
        {
            // Check to see if we are freeing managed resources.
            if (disposing)
            {
                // Free managed resources on the object.
                this.FreeManagedObjects();
            }

            // Free unmanaged resources on the object.
            this.FreeUnmanagedObjects();
        }
    }

    /// <summary>
    /// Asynchronously disposes the object and releases its resources.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        // Check to see if the object has been disposed or not.
        if (!this.isDisposed)
        {
            // Call the asynchronous dispose function, and free managed resources manually.
            await this.DisposeAsync(disposing: true);

            // Ensure thread-safe disposal by acquiring the lock.
            lock (this.@lock)
            {
                // Call the dispose function, and free managed resources manually.
                this.Dispose(disposing: true);

                // Mark the object as disposed.
                this.isDisposed = true;

                // The object has been disposed already, tell the GC we don't need to call the finalizer.
                GC.SuppressFinalize(this);
            }
        }
    }
    /// <summary>
    /// Asynchronously disposes resources of the object.
    /// This is called by <see cref="DisposeAsync()"/> to release managed and unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing">Indicates whether the method is being called from the <see cref="DisposeAsync()"/> method (true) or the finalizer (false).</param>
    /// <returns>A task representing the asynchronous disposal operation.</returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        // Check to see if the object has been disposed or not.
        if (!this.isDisposed)
        {
            // Check to see if we are freeing managed resources.
            if (disposing)
            {
                // Free managed resources on the object.
                await this.FreeManagedObjectsAsync();
            }

            // Free unmanaged resources on the object.
            await this.FreeUnmanagedObjectsAsync();
        }
    }

    /// <summary>
    /// Frees managed resources when the object is disposed.
    /// </summary>
    protected virtual void FreeManagedObjects() { }
    /// <summary>
    /// Frees unmanaged resources when the object is disposed.
    /// </summary>
    protected virtual void FreeUnmanagedObjects() { }
    /// <summary>
    /// Asynchronously frees managed resources when the object is disposed.
    /// </summary>
    /// <returns>A task representing the asynchronous cleanup operation.</returns>
    protected virtual async ValueTask FreeManagedObjectsAsync() => await ValueTask.CompletedTask;
    /// <summary>
    /// Asynchronously frees unmanaged resources when the object is disposed.
    /// </summary>
    /// <returns>A task representing the asynchronous cleanup operation.</returns>
    protected virtual async ValueTask FreeUnmanagedObjectsAsync() => await ValueTask.CompletedTask;
}