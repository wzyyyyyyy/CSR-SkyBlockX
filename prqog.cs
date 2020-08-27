using CSR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using System.Configuration;

namespace sbx
{
    class sbbx
    {
        public static void aaascr(MCCSAPI api)

        {
            api.setCommandDescribe("is creative", "创建一个空岛");
            api.setCommandDescribe("is goreset", "重置空岛传送点");
            api.setCommandDescribe("is go", "保存或传送到空岛传送点");
            api.setCommandDescribe("is sign", "签到");
            api.setCommandDescribe("is clear", "清理掉落物品");
            string qdpath = "./skyblockX/sign/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.DayOfYear.ToString();
            string qdjlpath = "./skyblockX/sign/签到奖励.txt";
            string _ifobsition = "true";
            string _ifrespawnpunish = "false";
            string eula = "false";
            string _ifautoclear = "false";
            string qdjl = "give @s apple";
            int cleartime = 10;
            int start_x = 0;
            int start_y = 0;
            int start_z = 0;
            int end_x = 0;
            int end_y = 0;
            int end_z = 0;
            int x_max = 5000;
            int z_max = 5000;
            if (File.Exists("./skyblockX/skyblockX.txt"))
            {
                try
                {
                    string[] config = File.ReadAllLines("./skyblockX/skyblockX.txt", System.Text.Encoding.Default);
                    _ifobsition = config[0].Substring(14);
                    _ifrespawnpunish = config[1].Substring(9);
                    _ifautoclear = config[2].Substring(7);
                    cleartime = int.Parse(config[3].Substring(7));
                    start_x = int.Parse(config[4].Substring(8));
                    start_y = int.Parse(config[5].Substring(8));
                    start_z = int.Parse(config[6].Substring(8));
                    end_x = int.Parse(config[7].Substring(6));
                    end_y = int.Parse(config[8].Substring(6));
                    end_z = int.Parse(config[9].Substring(6));
                    x_max = int.Parse(config[10].Substring(6));
                    z_max = int.Parse(config[11].Substring(6));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[SkyBlockX]配置文件读取成功！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]配置文件读取失败！");
                    Console.ForegroundColor = ConsoleColor.White;


                }

            }
            else
            {
                Directory.CreateDirectory("./skyblockX");
                File.AppendAllText("./skyblockX/skyblockX.txt", "是否允许桶点黑曜石变为岩浆:true\n是否开启重生惩罚:false\n是否自动清理:false\n自动清理间隔:10\nstart_x:0\nstart_y:0\nstart_z:0\nend_x:0\nend_y:0\nend_z:0\nx_max:5000\nz_max:5000", System.Text.Encoding.Default);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[SkyBlockX]未检查到配置文件！将自动创建！");
                Console.ForegroundColor = ConsoleColor.White;
            }
            int ktime = 60000 * cleartime;
            if (File.Exists("./skyblockX/islands"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[SkyBlockX]数据读取成功成功！");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                try
                {
                    System.IO.Directory.CreateDirectory("./skyblockX/islands");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[SkyBlockX]文件夹创建成功！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]文件夹创建失败！");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            if (File.Exists("./skyblockX/spawns"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[SkyBlockX]数据读取成功成功！");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                try
                {
                    System.IO.Directory.CreateDirectory("./skyblockX/spawns");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[SkyBlockX]文件夹创建成功！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]文件夹创建失败！");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            if(File.Exists(qdpath))
            {

            }
            else
            {
                try
                {
                    System.IO.Directory.CreateDirectory(qdpath);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[SkyBlockX]文件夹创建成功！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]文件夹创建失败！");
                    Console.ForegroundColor = ConsoleColor.White;
                    Directory.CreateDirectory(qdpath);
                }
                
            }
            if (File.Exists("./SkyBlockXeula.txt"))
            {
                try
                {
                    string[] config = File.ReadAllLines("./SkyBlockXeula.txt", System.Text.Encoding.Default);
                    eula = config[0].Substring(5); 
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[SkyBlockX]eula协议读取成功！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]eula协议读取失败！");
                    Console.ForegroundColor = ConsoleColor.White;


                }
            }
            else
            {
                File.AppendAllText("./SkyBlockXeula.txt", "eula=false\nAugust 2020 SkyBlockX Release.\nThanks for using SkyBlockX.\n一条重要规定是除非Sbaoor明确同意，\n否则您不得分发使用SkyBlockX创建的任何内容。\n\"分发使用SkyBlockX创建的任何内容\"是指：\n1.向任何其他人提供使用SkyBlockX的服务端整合包；\n2.将SkyBlockX用于商业用途；\n3.试图通过SkyBlockX创建的任何内容赚钱;\n如果您同意了eula协定，\n这代表您已被授予使用的许可，\n因此您可以在自己的服务端上使用它。\n", System.Text.Encoding.Default);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[SkyBlockX]未检查到eula文件！将自动创建！");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if(eula != "true")
            {
                while(true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyblockX]请同意用户协议");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                }
            }
            Dictionary<string, string> uuid = new Dictionary<string, string>();
            api.addAfterActListener(EventKey.onLoadName, x =>
            {
                var a = BaseEvent.getFrom(x) as LoadNameEvent;
                uuid.Add(a.playername, a.uuid);
                return true;
            });
            api.addBeforeActListener(EventKey.onPlayerLeft, x =>
            {
                var a = BaseEvent.getFrom(x) as PlayerLeftEvent;
                uuid.Remove(a.playername);
                return true;
            });
            api.addBeforeActListener(EventKey.onUseItem, x =>
            {
                var a = BaseEvent.getFrom(x) as UseItemEvent;
                string obsidian = "minecraft:obsidian";
                string Bucket = "Bucket";
                //Console.WriteLine("{0} {1}", a.blockname, a.itemname);
                if (a.blockname == obsidian & a.itemname == Bucket & a.dimensionid == 0 & _ifobsition == "true")
                {
                    api.runcmd("setblock " + a.position.x + " " + a.position.y + " " + a.position.z + " flowing_lava");
                    api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3已帮你恢复岩浆！\"}]}");

                }
                if (a.blockname == obsidian & a.itemname == Bucket & a.dimensionid == 0 & _ifobsition == "false")
                {
                    api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3岩浆还原已被禁用！\"}]}");
                }
                if (a.blockname == obsidian & a.itemname == Bucket & a.dimensionid == 0 & _ifobsition != "false" & _ifobsition != "true")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]配置文件填写错误！请检查是否允许岩浆还原项！");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return true;
            });
            api.addBeforeActListener(EventKey.onRespawn, x =>
            {
                var a = BaseEvent.getFrom(x) as RespawnEvent;
                if (_ifrespawnpunish == "true")
                {
                    api.runcmd("effect "+a.playername+ " hunger 4 225 true");
                }
                if (_ifrespawnpunish == "false")
                {

                }
                if (_ifrespawnpunish != "false" & _ifrespawnpunish != "true")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[SkyBlockX]配置文件填写错误！请检查是否重生惩罚项！");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return true;
            });
            api.addBeforeActListener(EventKey.onInputCommand, x =>
            {
            var a = BaseEvent.getFrom(x) as InputCommandEvent;
            string ispath = "./skyblockx/islands/" + a.playername + ".txt";
            string sppath = "./skyblockX/spawns/" + a.playername + ".txt";
            if(a.cmd.StartsWith("/is"))
            {
                    string iscommand = string.Empty;
                    iscommand = a.cmd.Substring(4);
                    if (iscommand == "creative")
                    {
                        if (File.Exists(ispath))
                        {

                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3你已经创建过了！！\"}]}");
                            return false;
                        }
                        else
                        {
                            Random ran = new Random();
                            int n = ran.Next(0, x_max);
                            int z = ran.Next(0, z_max);
                            api.runcmd("tp \"" + a.playername + "\" " + n + " 100 " + z);
                            api.runcmd("effect \"" + a.playername + "\" slow_falling 10 5 true");
                            StreamWriter writer = new StreamWriter(ispath);
                            writer.Write(string.Concat("坐标:" + n + " 70 " + z, Environment.NewLine));
                            writer.Close();
                            //Thread.Sleep(5000);
                            var t = Task.Run(async delegate
                            {
                                await Task.Delay(3000);
                                api.runcmd("clone " + start_x + " " + start_y + " " + start_z + " " + end_x + " " + end_y + " " + end_z + " " + n + " 50 " + z);
                                return 42;
                            });
                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3创建成功！\"}]}");
                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3请用/is go来保存你的传送点！\"}]}");
                            return false;
                        }

                    }
                    if (iscommand == "go")
                    {
                        if (File.Exists(sppath))
                        {
                            string[] config = File.ReadAllLines(sppath, System.Text.Encoding.Default);
                            int sp_x = int.Parse(config[0].Substring(2));
                            int sp_y = int.Parse(config[1].Substring(2));
                            int sp_z = int.Parse(config[2].Substring(2));
                            api.teleport(uuid[a.playername], sp_x, sp_y, sp_z, 0);
                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3传送成功！\"}]}");
                            return false;
                        }
                        else
                        {
                            StreamWriter writer = new StreamWriter(sppath);
                            int intx = Convert.ToInt32(a.XYZ.x);
                            int inty = Convert.ToInt32(a.XYZ.y);
                            int intz = Convert.ToInt32(a.XYZ.z);
                            writer.Write(string.Concat("x:" + intx, Environment.NewLine));
                            writer.Write(string.Concat("y:" + inty, Environment.NewLine));
                            writer.Write(string.Concat("z:" + intz, Environment.NewLine));
                            writer.Close();
                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3已保存空岛传送点！\"}]}");
                            return false;
                        }
                    }
                    if (iscommand == "goreset")
                    {
                        StreamWriter writer = new StreamWriter(sppath);
                        int intx = Convert.ToInt32(a.XYZ.x);
                        int inty = Convert.ToInt32(a.XYZ.y);
                        int intz = Convert.ToInt32(a.XYZ.z);
                        writer.Write(string.Concat("x:" + intx, Environment.NewLine));
                        writer.Write(string.Concat("y:" + inty, Environment.NewLine));
                        writer.Write(string.Concat("z:" + intz, Environment.NewLine));
                        writer.Close();
                        api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3已重置空岛传送点！\"}]}");
                        return false;
                    }
                    if (iscommand == "clear")
                    {
                        var b = Tools.Player.getPlayerAbilities(api, uuid[a.playername]);
                        if (b.op == "true")
                        {
                            var t = Task.Run(async delegate
                            {
                                api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3还有10秒就要清理掉落物品了！\"}]}");
                                await Task.Delay(10000);
                                api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3清理完毕！\"}]}");
                                api.runcmd("kill @e[type=item]");
                                return 42;
                            });
                        }
                        else
                        {
                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3权限不足！\"}]}");
                        }
                        return false;
                    }
                    if (iscommand == "sign")
                    {

                        string grqdpath = "./skyblockX/sign/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.DayOfYear.ToString() + "/" + a.playername + ".txt";
                        if (File.Exists(qdjlpath))
                        {
                            if (File.Exists(grqdpath))
                            {
                                api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3你签到过了！\"}]}");
                                return false;
                            }
                            else
                            {
                                if (File.Exists(qdpath))
                                {
                                    string[] config = File.ReadAllLines(qdjlpath, System.Text.Encoding.Default);
                                    qdjl = config[0].Substring(5);
                                    if (qdjl != null)
                                    {
                                        api.runcmd("execute \"" + a.playername + "\" ~~~ " + qdjl);
                                        api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3签到成功！\"}]}");
                                        StreamWriter writer = new StreamWriter(grqdpath);
                                        writer.Write(string.Concat(DateTime.Now.ToString("hh:mm:ss"), Environment.NewLine));
                                        writer.Close();
                                        return false;
                                    }
                                    else
                                    {
                                        api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3你的服主还没设置签到奖励！\"}]}");
                                        return false;
                                    }
                                }
                                else
                                {
                                    Directory.CreateDirectory("./skyblockX/sign/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.DayOfYear.ToString());
                                    string[] config = File.ReadAllLines(qdjlpath, System.Text.Encoding.Default);
                                    qdjl = config[0].Substring(5);
                                    if (qdjl != null)
                                    {
                                        api.runcmd("execute \"" + a.playername + "\" ~~~ " + qdjl);
                                        api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3签到成功！\"}]}");
                                        StreamWriter writer = new StreamWriter(grqdpath);
                                        writer.Write(string.Concat(DateTime.Now.ToString("hh:mm:ss"), Environment.NewLine));
                                        writer.Close();
                                        return false;
                                    }
                                    else
                                    {
                                        api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3你的服主还没设置签到奖励！\"}]}");
                                        return false;
                                    }

                                }

                            }
                        }
                        else
                        {

                            api.runcmd("tellraw \"" + a.playername + "\" {\"rawtext\":[{\"text\":\"§3你的服主还没设置签到奖励！\"}]}");
                            Directory.CreateDirectory("/skyblockX/sign");
                            File.AppendAllText(qdjlpath, "签到奖励：", System.Text.Encoding.Default);
                            return false;
                        }
                    }
            }

                return true;
            });
            if (_ifautoclear == "true")
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(ktime);
                        api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3还有10秒就要清理掉落物品了！\"}]}");
                        Thread.Sleep(10000);
                        api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3清理完毕！\"}]}");
                        api.runcmd("kill @e[type=item]");
                    }
                });
            }
            api.addBeforeActListener(EventKey.onServerCmd, x =>
            {
                var commandinput = BaseEvent.getFrom(x) as ServerCmdEvent;
                if (commandinput.cmd == "skyreload")
                {
                    try
                    {
                        string[] config = File.ReadAllLines("./skyblockX/skyblockX.txt", System.Text.Encoding.Default);
                        _ifobsition = config[0].Substring(14);
                        _ifrespawnpunish = config[1].Substring(9);
                        _ifautoclear = config[2].Substring(7);
                        cleartime = int.Parse(config[3].Substring(7));
                        start_x = int.Parse(config[4].Substring(8));
                        start_y = int.Parse(config[5].Substring(8));
                        start_z = int.Parse(config[6].Substring(8));
                        end_x = int.Parse(config[7].Substring(6));
                        end_y = int.Parse(config[8].Substring(6));
                        end_z = int.Parse(config[9].Substring(6));
                        x_max = int.Parse(config[10].Substring(6));
                        z_max = int.Parse(config[11].Substring(6));
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[SkyBlockX]配置文件读取成功！");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[SkyBlockX]配置文件读取失败！");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    return false;
                }
                if(commandinput.cmd == "stop")
                {
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3还有30秒就要关服了！\"}]}");
                            Thread.Sleep(10000);
                            api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3还有20秒就要关服了！\"}]}");
                            Thread.Sleep(10000);
                            api.runcmd("tellraw @a {\"rawtext\":[{\"text\":\"§3还有10秒就要关服了！\"}]}");
                            Thread.Sleep(10000);
                            api.runcmd("stop");
                        }
                    });
                    return false;
                }
                return true;
            });
        }
    }
}
namespace CSR
{
    partial class Plugin
    {

        public static void onStart(MCCSAPI api)
        {
            // TODO 此接口为必要实现
            sbx.sbbx.aaascr(api);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[SkyBlockX]空岛核心已加载！");
            Console.WriteLine(@" ____    __               ____    ___                   __      __   __     ");
            Console.WriteLine(@"/\  _`\ /\ \             /\  _`\ /\_ \                 /\ \    /\ \ /\ \    ");
            Console.WriteLine(@"\ \,\L\_\ \ \/'\   __  __\ \ \L\ \//\ \     ___     ___\ \ \/'\\ `\`\/'/'   ");
            Console.WriteLine(@" \/_\__ \\ \ , <  /\ \/\ \\ \  _ <'\ \ \   / __`\  /'___\ \ , < `\/ > <     ");
            Console.WriteLine(@"   /\ \L\ \ \ \\`\\ \ \_\ \\ \ \L\ \\_\ \_/\ \L\ \/\ \__/\ \ \\`\  \/'/\`\  ");
            Console.WriteLine(@"   \ `\____\ \_\ \_\/`____ \\ \____//\____\ \____/\ \____\\ \_\ \_\/\_\\ \_\");
            Console.WriteLine(@"    \/_____/\/_/\/_/`/___/> \\/___/ \/____/\/___/  \/____/ \/_/\/_/\/_/ \/_/");
            Console.WriteLine(@"                       /\___/                                               ");
            Console.WriteLine(@"                       \/__/                                                ");
            Console.WriteLine("[SkyBlockX]bug反馈请加QQ:2023786106");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}