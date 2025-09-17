using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectCell_HMI.Forms
{
    public partial class ceshi : UserControl
    {
        public bool isRuningAsync = false;
        public List<double[]> allCombinations;
        public string fnExe;
        public int numCaseFinished;
        public ceshi()
        {
            InitializeComponent();

            allCombinations = new List<double[]>();
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            string directory = Path.GetDirectoryName(exePath);
            this.fnExe = directory + @"\aeSLN.exe";

            // 读取AutoTestConfig.ini文件并显示到dataGridViewAutoTest
            string autoTestConfigPath = @"AutoTestConfig.ini";
            try
            {
                LoadAutoTestConfig(autoTestConfigPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取AutoTestConfig.ini文件时出错: " + ex.Message);
            }
        }

        public void LoadAutoTestConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"文件 {filePath} 不存在！");
                return;
            }

            // 初始化dataGridViewAutoTest
            InitializeAutoTestDataGridView();

            // 读取文件内容
            string[] lines = File.ReadAllLines(filePath);
            int rowIndex = 1;
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue; // 跳过空行
                }

                // 以逗号分隔
                string[] parts = line.Split(',');
                // 插入序号到第一列
                var rowData = new List<object> { rowIndex++ };
                rowData.AddRange(parts);
                // 将整行数据添加到DataGridView中
                dataGridView2.Rows.Add(rowData.ToArray());
            }
        }

        public void InitializeAutoTestDataGridView()
        {
            // 清空现有列和行
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // 禁用行表头
            dataGridView2.RowHeadersVisible = false;

            // 添加序号列
            dataGridView2.Columns.Add("col0", "序号");
            dataGridView2.Columns.Add("col1", "类型");
            dataGridView2.Columns.Add("col2", "编号");
            dataGridView2.Columns.Add("col3", "成分");
            dataGridView2.Columns.Add("col4", "最小");
            dataGridView2.Columns.Add("col5", "最大");
            dataGridView2.Columns.Add("col6", "个数");

            // 调整列宽以美化显示
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public class ConfigEntry
        {
            public string Type { get; set; }

            public string Id { get; set; }

            public string GasType { get; set; }

            public override string ToString()
            {
                return $"类型: {Type}, ID: {Id}, 气体类型: {GasType}";
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // 定义你的INI文件路径
            string filePath = "AutoTestConfig.ini";

            try
            {
                // 调用方法读取和解析文件
                List<ConfigEntry> configEntries = ReadIniFile(filePath);

                // 打印结果以验证
                richTextBox1.AppendText("成功读取并解析配置文件：\r\n");
                foreach (var entry in configEntries)
                {
                    richTextBox1.AppendText(entry + "\r\n");
                    richTextBox1.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                // 错误处理 
                richTextBox1.AppendText($"处理文件时发生错误: {ex.Message}");
                richTextBox1.ScrollToCaret();
            }
            try
            {
                List<double[]> numberSequences = ExtractNumberSequencesFromFile(filePath);

                richTextBox1.AppendText("成功从文件中提取并生成了以下数列：\r\n");
                int i = 1;
                foreach (var seq in numberSequences)
                {
                    // 使用string.Join将数组格式化为逗号分隔的字符串以便查看
                    richTextBox1.AppendText($"数列 {i++}: [ {string.Join(", ", seq)} ]\r\n");
                    richTextBox1.ScrollToCaret();
                }

                // 1. 计算总组合数
                long totalCombinations = GetTotalCombinationsCount(numberSequences);
                richTextBox1.AppendText($"列表中共有 {numberSequences.Count} 个数组。\r\n");
                richTextBox1.AppendText($"所有可能的取法（组合数）总计为: {totalCombinations:N0}\r\n"); // :N0 用于格式化数字，如 1,234,567

                if (totalCombinations == 0)
                {
                    richTextBox1.AppendText("没有可生成的组合。\r\n");
                }
                else
                {
                    // 2. 遍历所有组合
                    richTextBox1.AppendText("\n开始遍历所有可能的组合：\r\n");
                    allCombinations = TraverseCombinations(numberSequences);

                    // 3. 打印结果
                    int j = 1;
                    foreach (var combo in allCombinations)
                    {
                        richTextBox1.AppendText($"组合 {j++}: [ {string.Join(", ", combo)} ]\r\n");
                        //richTextBox1.ScrollToCaret();
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"处理文件时发生错误: {ex.Message}\r\n");
            }
        }

        public static List<ConfigEntry> ReadIniFile(string filePath)
        {
            var entries = new List<ConfigEntry>();

            // 使用File.ReadLines，它可以有效地逐行读取文件，特别适用于大文件
            foreach (string line in File.ReadLines(filePath))
            {
                // 如果行为空或仅包含空白字符，则跳过
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // 使用逗号作为分隔符来分割行
                string[] parts = line.Split(',');

                // 检查分割后是否至少有3个部分，以避免索引越界异常
                if (parts.Length >= 3)
                {
                    // 创建一个新的ConfigEntry对象并填充数据
                    ConfigEntry entry = new ConfigEntry
                    {
                        // 使用Trim()来移除可能存在的多余空格
                        Type = parts[0].Trim(),
                        Id = parts[1].Trim(),
                        GasType = parts[2].Trim()
                    };

                    // 将新创建的对象添加到列表中
                    entries.Add(entry);
                }
            }

            return entries;
        }

        public static List<double[]> ExtractNumberSequencesFromFile(string filePath)
        {
            var listOfSequences = new List<double[]>();

            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // 分割并移除所有空条目
                string[] allParts = line.Split(',');
                var nonEmptyParts = allParts.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

                // 我们需要至少6个非空部分来提取前三个和后三个
                // 假设前三个是你之前关心的 (type, id, gasType)
                // 后三个是 (min, max, count)
                if (nonEmptyParts.Length >= 6)
                {
                    try
                    {
                        // 解析后三个参数
                        // 这里我们取倒数第三、第二、第一个元素，这样更稳健
                        double min = double.Parse(nonEmptyParts[nonEmptyParts.Length - 3]);
                        double max = double.Parse(nonEmptyParts[nonEmptyParts.Length - 2]);
                        int count = int.Parse(nonEmptyParts[nonEmptyParts.Length - 1]);

                        // 根据你的描述，这些参数在第4、5、6个位置（索引为3, 4, 5）
                        // double min = double.Parse(nonEmptyParts[3]);
                        // double max = double.Parse(nonEmptyParts[4]);
                        // int count = int.Parse(nonEmptyParts[5]);


                        // 调用辅助方法生成数列
                        double[] sequence = GenerateSequence(min, max, count);

                        // 将生成的数列添加到列表中
                        listOfSequences.Add(sequence);
                    }
                    catch (FormatException ex)
                    {
                        // 如果某行的数据格式不正确（例如，无法转换为数字），则打印警告并跳过该行
                        MessageBox.Show($"警告: 无法解析行 '{line}' 中的数字。错误: {ex.Message}. 已跳过此行。");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show($"警告: 处理行 '{line}' 时参数无效。错误: {ex.Message}. 已跳过此行。");
                    }
                }
            }

            return listOfSequences;
        }

        public static double[] GenerateSequence(double min, double max, int count)
        {
            // 参数校验
            if (count < 0)
            {
                throw new ArgumentException("数量不能为负数。");
            }
            if (count == 0)
            {
                return new double[0]; // 返回一个空数组
            }
            // 如果count为1，数列只包含一个元素
            if (count == 1)
            {
                return new double[] { min };
            }
            if (min > max && count > 1)
            {
                throw new ArgumentException("当数量大于1时，最小值不能大于最大值。");
            }

            var sequence = new double[count];
            // 计算步长。当count > 1时，区间个数为 count - 1
            double step = (max - min) / (count - 1);

            for (int i = 0; i < count; i++)
            {
                // 计算数列中的每一个值
                sequence[i] = min + (step * i);
            }

            // 由于浮点数计算可能存在微小的精度误差，我们强制将最后一个元素设置为max，确保准确性。
            sequence[count - 1] = max;

            return sequence;
        }

        public static long GetTotalCombinationsCount(List<double[]> sequences)
        {
            // 如果列表为空或任何一个数组为空，则没有组合
            if (sequences == null || sequences.Count == 0 || sequences.Any(arr => arr.Length == 0))
            {
                return 0;
            }

            // 使用long类型以防止乘积过大导致整数溢出
            long totalCount = 1;
            foreach (var arr in sequences)
            {
                // 使用checked关键字可以在发生溢出时抛出异常
                checked
                {
                    totalCount *= arr.Length;
                }
            }
            return totalCount;
        }

        public static List<double[]> TraverseCombinations(List<double[]> sequences)
        {
            var allCombinations = new List<double[]>();
            if (sequences == null || sequences.Count == 0)
            {
                return allCombinations;
            }

            var currentCombination = new List<double>();
            // 启动递归
            FindCombinationsRecursive(0, currentCombination, sequences, allCombinations);
            return allCombinations;
        }

        public static void FindCombinationsRecursive(int depth, List<double> currentCombination, List<double[]> sequences, List<double[]> allCombinations)
        {
            // **基准情形 (Base Case)**:
            // 如果深度达到了数列列表的大小，说明我们已经从每个数组中都选择了一个元素。
            // 一个完整的组合已经形成。
            if (depth == sequences.Count)
            {
                // 将当前组合的副本添加到最终列表中。
                // 必须使用 .ToArray() 创建副本，否则后续的回溯操作会修改已添加的列表。
                allCombinations.Add(currentCombination.ToArray());
                return;
            }

            // **递归步骤 (Recursive Step)**:
            // 遍历当前深度（depth）对应的数组中的每一个元素。
            for (int i = 0; i < sequences[depth].Length; i++)
            {
                // 1. **选择 (Choose)**: 将当前元素添加到正在构建的组合中。
                currentCombination.Add(sequences[depth][i]);

                // 2. **探索 (Explore)**: 对下一个深度的数组进行递归调用。
                FindCombinationsRecursive(depth + 1, currentCombination, sequences, allCombinations);

                // 3. **回溯 (Backtrack/Unchoose)**:
                //    当从更深层的递归返回后，移除刚刚添加的元素。
                //    这样，在下一次循环中，我们就可以为当前深度选择下一个不同的元素。
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (this.allCombinations.Count == 0)
            {
                MessageBox.Show("先确认测试方案");
                return;
            }
            timer1.Enabled = true;
            richTextBox1.Clear();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            int numCase = this.allCombinations.Count;
            if (this.isRuningAsync)
            {
                return;
            }
            else
            {
                if (this.numCaseFinished < numCase)
                {
                    this.numCaseFinished++;
                    richTextBox1.AppendText("开始执行测试样本" + this.numCaseFinished.ToString() + "...\r\n");
                    System.IO.File.Delete(@"debug.flg");
                    RunExeAsync(fnExe);
                }
                else
                {
                    timer1.Enabled = false;
                    int tmp_ = this.numCaseFinished;
                    this.numCaseFinished = 0;
                    MessageBox.Show("自动测试完成,共" + tmp_.ToString() + "个案例");
                }
            }
        }

        public async void RunExeAsync(string fnExe)
        {
            this.isRuningAsync = true;
            string commandToRun = fnExe;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = commandToRun;
            startInfo.WorkingDirectory = fnExe.Substring(0, fnExe.Length - "\\aeSLN.exe".Length);

            // 重定向输出流，以便捕获程序的输出
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true; // 不显示控制台窗口

            try
            {
                Process process = new Process();
                process.StartInfo = startInfo;

                // 订阅输出数据接收事件
                process.OutputDataReceived += (s, args) =>
                {

                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        // 使用Invoke确保在UI线程上更新richTextBox1
                        this.Invoke(new Action(() =>
                        {
                            if (args.Data.ToString().Contains("itime"))
                            {
                                GetTimeStamp(args.Data.ToString(), out int itime, out double time_t);
                                UpdateInfo("步数" + itime.ToString() + " 时间t=" + time_t.ToString() + 's');
                            }

                            if (args.Data.ToString().Contains("#Flag#"))//args.Data.ToString().StartsWith("#Flag#")
                            {
                                richTextBox1.AppendText("\r\n" + args.Data.ToString());
                                richTextBox1.ScrollToCaret(); // 自动滚动到最新内容
                                if (args.Data.ToString().Contains("Waiting for injection"))
                                {
                                    CreateFlagFile();
                                }
                            }
                        }));
                    }
                };

                // 订阅错误输出数据接收事件
                process.ErrorDataReceived += (s, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        // 使用Invoke确保在UI线程上更新richTextBox1
                        this.Invoke(new Action(() =>
                        {
                            richTextBox1.AppendText("[错误] " + args.Data + "\r\n");
                            richTextBox1.ScrollToCaret();
                        }));
                    }
                };

                // 启动进程
                process.Start();
                // 开始异步读取输出
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                richTextBox1.AppendText("进程已启动，等待执行完成...\r\n");

                // 异步等待进程完成
                await Task.Run(() => process.WaitForExit());
                // 获取退出代码
                int exitCode = process.ExitCode;
                // 在UI线程上更新最终状态
                this.Invoke(new Action(() =>
                {
                    if (exitCode == 0)
                    {
                        richTextBox1.AppendText($"\r\n仿真程序 执行完成！退出代码 {exitCode}\r\n");
                        // reNewButtons();
                        this.isRuningAsync = false;
                        richTextBox1.ScrollToCaret(); // 自动滚动到最新内容
                        // MessageBox.Show("aeSLN.exe 执行完成");
                    }
                    else
                    {
                        richTextBox1.AppendText($"\r\n仿真程序 执行完成，但可能有错误。退出代码 {exitCode}\r\n");
                        //MessageBox.Show($"仿真程序 执行完成，但可能有错误。退出代码 {exitCode}");
                        richTextBox1.ScrollToCaret(); // 自动滚动到最新内容
                    }
                }));
                // 释放资源
                process.Dispose();
            }

            catch (Win32Exception ex)
            {
                string errorMsg = $"启动失败: 找不到'{commandToRun}'。请确保文件存在且路径正确。错误信息 {ex.Message}";
                richTextBox1.AppendText($"[错误] {errorMsg}\r\n");
                //MessageBox.Show(errorMsg);
            }
            catch (Exception ex)
            {
                string errorMsg = $"启动时发生未知错误 {ex.Message}";
                richTextBox1.AppendText($"[错误] {errorMsg}\r\n");
                //MessageBox.Show(errorMsg);
            }
        }

        public void GetTimeStamp(string input, out int itime, out double time_t)
        {
            // 提取 itime 的值（整数）
            Match itimeMatch = Regex.Match(input, @"itime\s*=\s*(\d+)");
            if (itimeMatch.Success && itimeMatch.Groups.Count > 1)
            {
                if (int.TryParse(itimeMatch.Groups[1].Value, out int itimeValue))
                {
                    itime = itimeValue;
                }
                else
                { itime = -1; }
            }
            else
            { itime = -1; }

            // 提取 time_t 的值（浮点数）
            Match time_tMatch = Regex.Match(input, @"time_t\s*=\s*([\d.]+)");
            if (time_tMatch.Success && time_tMatch.Groups.Count > 1)
            {
                if (double.TryParse(time_tMatch.Groups[1].Value, out double time_tValue))
                {
                    time_t = time_tValue;
                }
                else { time_t = -1; }
            }
            else { time_t = -1; }
        }

        public void UpdateInfo(string info_line)
        {
            int lineCount = richTextBox1.Lines.Length;
            if (lineCount > 0)
            {
                // 获取最后一行的起始位置
                int lastLineStart = richTextBox1.GetFirstCharIndexFromLine(lineCount - 1);

                // 删除最后一行的内容（从起始位置到文本末尾）
                richTextBox1.Select(lastLineStart, richTextBox1.TextLength - lastLineStart);
                richTextBox1.SelectedText = "";

                // 添加新的最后一行内容（不添加换行符）
                richTextBox1.AppendText(info_line);
            }
        }

        public bool CreateFlagFile()
        {
            string filePath = @"debug.flg";
            try
            {
                // 使用 using 语句创建文件。
                // File.Create 返回一个 FileStream，using 代码块结束后，
                // 会自动调用其 Dispose() 方法，从而关闭文件流。
                using (FileStream fs = File.Create(filePath))
                {
                    // 文件已创建，我们不需要向其中写入任何内容，
                    // using 语句结束时它会自动关闭，成为一个0字节的文件。
                }

                // Console.WriteLine("文件 'debug.flg' 创建成功！");
            }
            catch (IOException ex)
            {
                // 如果文件已存在或发生其他IO错误，这里会捕获到异常
                MessageBox.Show("创建文件时出错: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // 捕获其他可能的异常
                // Console.WriteLine("发生未知错误: " + ex.Message);
                MessageBox.Show("发生未知错误: " + ex.Message);
                return false;
            }
            return true;
        }


    }
}
