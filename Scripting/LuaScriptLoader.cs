using Reoria.Engine.Scripting.Interfaces;

namespace Reoria.Engine.Scripting;

public class LuaScriptLoader : IScriptLoader
{
    public virtual Stream OpenStream(string path)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(path));

        byte[] fileBytes = File.ReadAllBytes(path);

        return new MemoryStream(fileBytes);
    }
}
