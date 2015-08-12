using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;

namespace Logger
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [Export(typeof(IRequestNotify))]
    [ExportMetadata("Guid", "093cf965-6eb2-b27e-46de-c60b1201e48a")]
    [ExportMetadata("Title", "KanColleLogger")]
    [ExportMetadata("Description", "File logging back-end")]
	[ExportMetadata("Version", "1.0.1")]
	[ExportMetadata("Author", "@Vernon")]
	public class KanColleLogger : IPlugin, ITool, IRequestNotify
    {
        private LoggerViewModel viewModel;

        string ITool.Name => "Logger";

        object ITool.View => new LoggerView { DataContext = this.viewModel, };

        public event EventHandler<NotifyEventArgs> NotifyRequested;

        public void Initialize()
        {
            this.viewModel = new LoggerViewModel
            {
                Loggers = new ObservableCollection<LoggerBase>
                {
                    new ItemLog(KanColleClient.Current.Proxy),
                    new ConstructionLog(KanColleClient.Current.Proxy),
                    new BattleLog(KanColleClient.Current.Proxy),
                    new MaterialsLog(KanColleClient.Current.Proxy),
                    new ExpedLog(KanColleClient.Current.Proxy),
                }
            };
        }
	}
}
