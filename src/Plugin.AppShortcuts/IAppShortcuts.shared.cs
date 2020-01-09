using System.Collections.Generic;

namespace Plugin.AppShortcuts
{
    /// <summary>
    /// Main interface for app shortcuts
    /// </summary>
    public interface IAppShortcuts
    {
        /// <summary>
        /// Initializes the plugin
        /// </summary>
        void Init();

        /// <summary>
        /// Gets a list of the current app shortcuts
        /// </summary>
        /// <returns>List of Shortcuts</returns>
        IEnumerable<Shortcut> GetShortcuts();

        /// <summary>
        /// Add a new app shortcut
        /// </summary>
        /// <param name="shortcut">Values for the shortcut</param>
        /// <returns></returns>
        void AddShortcut(Shortcut shortcut);

        /// <summary>
        /// Removes an app shortcut
        /// </summary>
        /// <param name="shortcutId">Id of shortcut to remove</param>
        /// <returns></returns>
        void RemoveShortcut(string shortcutId);
    }
}
