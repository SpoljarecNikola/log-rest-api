using Log_REST_Api.DataModel;

namespace Log_REST_Api.Data
{
    public class Seed
    {

        public List<Log>? Logs { get; private set; }

        public Seed() {
            Logs = new List<Log> {
                    new Log {
                        Timestamp = Convert.ToDateTime("2020-11-04 00:25:56.486 +01:00"),
                        Level = (Enums.LogEventLevel)3,
                        AppId = new Guid("E2C67223-862D-40D5-B85E-47D75BF7C4A3"),
                        Module = "Extended property builder",
                        Message = "Error in Extended property builder module on source object (113-39952)."
                    },{ new Log {
                            Timestamp = Convert.ToDateTime("2020-11-04 02:29:27.600 +01:00"),
                            Level = (Enums.LogEventLevel)2,
                            AppId = new Guid("E2C67223-862D-40D5-B85E-47D75BF7C4A3"),
                            Module = "Extended property operations",
                            Message = "Rule Dogodek - klic - Priložnost Aktivna z Ponudba updated object (113-40073)."}
                    }, { new Log {
                            Timestamp = Convert.ToDateTime("2020-11-04 02:29:31.991 +01:00"),
                            Level = (Enums.LogEventLevel)2,
                            AppId = new Guid("E2C67223-862D-40D5-B85E-47D75BF7C4A3"),
                            Module = "Extended property operations",
                            Message = "Rule Dogodek - klic - Priložnost Aktivna z Ponudba updated object (113-18225)."}
                    }, { new Log {
                            Timestamp = Convert.ToDateTime("2020-11-04 03:59:25.994 +01:00"),
                            Level = (Enums.LogEventLevel)2,
                            AppId = new Guid("E2C67223-862D-40D5-B85E-47D75BF7C4A3"),
                            Module = "Extended e-mail notifications",
                            Message = "Sent e-mail message Subject: [Obvestilo] - Priložnost brez ponudbe, v 48 urah ni vpisa dogodka - 14730, TO: Admir.Taranis@company.com, CC: Nina.Kasparov@company.com, Janez.Jansa@company.com, BCC: Dave.Pierce@company.com for rule Obvestilo - brez vpisa dogodka po 48 urah on object (113-40134)."}
                    }, { new Log {
                            Timestamp = Convert.ToDateTime("2020-11-04 05:59:29.041 +01:00"),
                            Level = (Enums.LogEventLevel)2,
                            AppId = new Guid("E2C67223-862D-40D5-B85E-47D75BF7C4A3"),
                            Module = "HTTP integration",
                            Message = "Rule Otvori TO-DO - 48ur ni Ponudbe [out] za Priložnost updated object (113-40135)." }

                    }
            };
        }
        public List<Log> GetLogs()
        {
            return Logs;
        }
    }
}
