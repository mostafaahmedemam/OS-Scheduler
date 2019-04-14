using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Process
        {
            public string process_num;
            public double arrival_time, burst_time;
            public int Priority;
            public bool marked = false;

            public void set(string pnum, double arrival_time, double burst_time, int Priority = 0)
            {
                process_num = pnum;
                this.arrival_time = arrival_time;
                this.burst_time = burst_time;
                this.Priority = Priority;
            }
        };

        static void Sort(List<Process> Processes, int no_Process)
        {
            double temp1;
            string temp2;

            for (int i = 0; i < no_Process - 1; i++)
            {
                for (int j = i + 1; j < no_Process; j++)
                {
                    if (Processes[i].burst_time > Processes[j].burst_time)
                    {
                        temp1 = Processes[i].burst_time;
                        Processes[i].burst_time = Processes[j].burst_time;
                        Processes[j].burst_time = temp1;

                        temp2 = Processes[i].process_num;
                        Processes[i].process_num = Processes[j].process_num;
                        Processes[j].process_num = temp2;

                        temp1 = Processes[i].arrival_time;
                        Processes[i].arrival_time = Processes[j].arrival_time;
                        Processes[j].arrival_time = temp1;
                    }

                    else if (Processes[i].burst_time == Processes[j].burst_time)
                    {
                        if (Processes[i].arrival_time > Processes[j].arrival_time)
                        {
                            temp1 = Processes[i].burst_time;
                            Processes[i].burst_time = Processes[j].burst_time;
                            Processes[j].burst_time = temp1;

                            temp2 = Processes[i].process_num;
                            Processes[i].process_num = Processes[j].process_num;
                            Processes[j].process_num = temp2;

                            temp1 = Processes[i].arrival_time;
                            Processes[i].arrival_time = Processes[j].arrival_time;
                            Processes[j].arrival_time = temp1;

                        }
                    }
                }
            }
        }

        static void Sort_Priority(List<Process> Processes, int no_Process)
        {
            double temp1;
            string temp2;


            for (int i = 0; i < no_Process - 1; i++)
            {
                for (int j = i + 1; j < no_Process; j++)
                {
                    if (Processes[i].Priority > Processes[j].Priority)
                    {
                        temp1 = Processes[i].Priority;
                        Processes[i].Priority = Processes[j].Priority;
                        Processes[j].Priority = (int)temp1;

                        temp2 = Processes[i].process_num;
                        Processes[i].process_num = Processes[j].process_num;
                        Processes[j].process_num = temp2;

                        temp1 = Processes[i].arrival_time;
                        Processes[i].arrival_time = Processes[j].arrival_time;
                        Processes[j].arrival_time = temp1;


                        temp1 = Processes[i].burst_time;
                        Processes[i].burst_time = Processes[j].burst_time;
                        Processes[j].burst_time = temp1;
                    }
                }
            }
        }

        static void Sort_Arrival(List<Process> Processes, int no_Process)
        {
            double temp1;
            string temp2;
            for (int i = 0; i < no_Process - 1; i++)
            {
                for (int j = i + 1; j < no_Process; j++)
                {
                    if (Processes[i].arrival_time > Processes[j].arrival_time)
                    {
                        temp1 = Processes[i].arrival_time;
                        Processes[i].arrival_time = Processes[j].arrival_time;
                        Processes[j].arrival_time = (int)temp1;

                        temp2 = Processes[i].process_num;
                        Processes[i].process_num = Processes[j].process_num;
                        Processes[j].process_num = temp2;

                        temp1 = Processes[i].burst_time;
                        Processes[i].burst_time = Processes[j].burst_time;
                        Processes[j].burst_time = temp1;
                    }
                }
            }
        }
        static List<Process> schedule = new List<Process>();
        /*/////////////////////////////////////// FCFS //////////////////////////////////////////////*/
        static double FCFS_Schedule(List<Process> Processes, int no_Process)
        {
            double waiting_Time = 0, current_time = 0;

            Sort_Arrival(Processes, no_Process);
            for (int i = 0; i < no_Process; i++)
            {
                Process temp_set = new Process();
                temp_set.set(Processes[i].process_num, current_time, Processes[i].burst_time);
                schedule.Add(temp_set);
                if (i != 0)
                    waiting_Time += current_time - Processes[i].arrival_time;
                current_time += Processes[i].burst_time;
            }
            return waiting_Time / no_Process;
        }
        /*//////////////////////////// Shortest Job First (Non Preemptive) ///////////////////////////////////*/
        static double SJFNP_Schedule(List<Process> Processes, int no_Process) //schedule
        {
            double waiting_Time = 0;
            double current_Time = 0;

            Sort(Processes, no_Process);

            for (int j = 0; j < no_Process; j++)
            {
                int i = 0;
                while (current_Time < Processes[i].arrival_time)
                {
                    i++;
                }

                Process temp_set = new Process();
                temp_set.set(Processes[i].process_num, current_Time, Processes[i].burst_time);
                schedule.Add(temp_set);

                waiting_Time += current_Time - Processes[i].arrival_time;
                current_Time += Processes[i].burst_time;
                Processes.RemoveAt(i);
            }
            return waiting_Time / no_Process;
        }

        /*///////////////////////////////////////// Shortest Job First (Preemptive) ///////////////////////////////////////////*/
        static double SJFP_Schedule(List<Process> Processes, int no_Process)
        {
            List<double> Waiting_Time = new List<double>();
            double waiting_Time = 0;
            double current_Time = 0;
            int j = 0;

            while (Processes.Any())
            {
                Sort(Processes, Processes.Count);
                int i = 0;
                while (current_Time < Processes[i].arrival_time)
                    i++;

                if (j == 0)
                    Waiting_Time.Add(0);

                else
                {
                    double temp = current_Time;
                    Waiting_Time.Add(temp - Processes[i].arrival_time);
                    waiting_Time += Waiting_Time[j];
                }
                Process temp_set = new Process();
                double pre_current_time = current_Time;
                double burst = Processes[i].burst_time;
                bool suspend = false;
                do
                {
                    current_Time++;
                    burst--;
                    for (int k = 0; k < i; k++)
                    {
                        if ((current_Time >= Processes[k].arrival_time) && (burst > Processes[k].burst_time))
                            suspend = true;
                    }
                } while (!suspend && burst != 0);

                if (suspend)
                {
                    temp_set.set(Processes[i].process_num, pre_current_time, (current_Time - pre_current_time), Processes[i].Priority); //gannt chart
                    schedule.Add(temp_set);

                    if (Processes[i].burst_time - (current_Time - pre_current_time) == 0)
                        Processes.RemoveAt(i);

                    else
                    {
                        Processes[i].arrival_time = current_Time;
                        Processes[i].burst_time -= current_Time - pre_current_time;
                    }

                }

                else
                {
                    temp_set.set(Processes[i].process_num, pre_current_time, Processes[i].burst_time, Processes[i].Priority);//gannt chart
                    schedule.Add(temp_set);
                    Processes.RemoveAt(i);
                }
                j++;
            }
            return waiting_Time / no_Process;
        }
        /*///////////////////////////////////////// Priority (Non Preemptive) ///////////////////////////////////////////////////*/
        static double PriorityNP_Schedule(List<Process> Processes, int no_Process)
        {
            double waiting_Time = 0;
            double current_Time = 0;

            Sort_Priority(Processes, no_Process);

            for (int j = 0; j < no_Process; j++)
            {
                int i = 0;
                while (current_Time < Processes[i].arrival_time)
                    i++;

                Process temp_set = new Process();
                temp_set.set(Processes[i].process_num, current_Time, Processes[i].burst_time);
                schedule.Add(temp_set);

                waiting_Time += current_Time - Processes[i].arrival_time;
                current_Time += Processes[i].burst_time;
                Processes.RemoveAt(i);
            }
            return waiting_Time / no_Process;
        }
        /*/////////////////////////////////////// Priority (Preemptive) ///////////////////////////////////////////////*/
        static double PriorityP_Schedule(List<Process> Processes, int no_Process)
        {
            List<double> Waiting_Time = new List<double>();
            double waiting_Time = 0;
            double current_Time = 0;
            int j = 0;

            Sort_Priority(Processes, Processes.Count);
            while (Processes.Any())
            {
                int i = 0;
                while (current_Time < Processes[i].arrival_time)
                    i++;

                if (j == 0)
                    Waiting_Time.Add(0);

                else
                {
                    Waiting_Time.Add(current_Time - Processes[i].arrival_time);
                    waiting_Time += Waiting_Time[j];
                }

                Process temp_set = new Process();

                double pre_current_time = current_Time;
                double burst = Processes[i].burst_time;
                bool suspend = false;
                do
                {
                    current_Time++;
                    burst--;
                    for (int k = 0; k < i; k++)
                    {
                        if (current_Time >= Processes[k].arrival_time)
                            suspend = true;
                    }
                } while (!suspend && burst != 0);

                if (suspend)
                {
                    temp_set.set(Processes[i].process_num, pre_current_time, current_Time - pre_current_time, Processes[i].Priority); //gannt chart
                    schedule.Add(temp_set);

                    if (Processes[i].burst_time - (current_Time - pre_current_time) == 0)
                        Processes.RemoveAt(i);

                    else
                    {
                        Processes[i].arrival_time = current_Time;
                        Processes[i].burst_time -= current_Time - pre_current_time;
                    }

                }
                else
                {
                    temp_set.set(Processes[i].process_num, pre_current_time, Processes[i].burst_time, Processes[i].Priority);//gannt chart
                    schedule.Add(temp_set);
                    Processes.RemoveAt(i);
                }
                j++;
            }
            return waiting_Time / no_Process;
        }
        /*////////////////////////////////////////////// Round Robin /////////////////////////////////////////////////////*/
        static double RR_Schedule(List<Process> Processes, int no_Process, int Quantum)
        {
            List<double> Waiting_Time = new List<double>();
            List<Process> readyqueue = new List<Process>();
            double waiting_Time = 0;
            double current_time = 0;
            double pre_Burst_time = 0;
            int j = 0;

            Sort_Arrival(Processes, no_Process);
            for (int i = 0; i < Processes.Count(); i++)
            {
                bool flag = false;
                if (Processes[i].arrival_time == 0)
                {
                    readyqueue.Add(Processes[i]);
                    Processes.RemoveAt(i);
                    i = i - 1;
                    flag = true;
                }

                if (!flag)
                    break;
            }

            while (readyqueue.Any())
            {
                for (int i = 0; i < readyqueue.Count(); i++)
                {
                    if (j == 0)
                        Waiting_Time.Add(0);

                    else
                    {
                        //double temp = Waiting_Time[j - 1] + schedule[j - 1].burst_time + schedule[j - 1].arrival_time;
                        Waiting_Time.Add(current_time - readyqueue[i].arrival_time);
                        waiting_Time += Waiting_Time[j];
                    }

                    if (readyqueue[i].burst_time <= Quantum)
                    {
                        pre_Burst_time = readyqueue[i].burst_time;
                        Process temp_set = new Process();
                        temp_set.set(readyqueue[i].process_num, current_time, pre_Burst_time);
                        schedule.Add(temp_set);
                        readyqueue.RemoveAt(i);
                        i--;
                        current_time += pre_Burst_time;

                    }
                    else
                    {
                        pre_Burst_time = Quantum;
                        Process temp_set = new Process();
                        temp_set.set(readyqueue[i].process_num, current_time, pre_Burst_time);
                        schedule.Add(temp_set);
                        current_time += pre_Burst_time;
                        readyqueue[i].arrival_time = current_time;
                        readyqueue[i].burst_time -= pre_Burst_time;
                        readyqueue[i].marked = true;
                    }
                    j++;
                    if (Processes.Any())
                    {
                        if (Processes[0].arrival_time <= current_time)
                        {
                            if (i < readyqueue.Count - 1 && readyqueue[i + 1].marked == true)
                            {
                                readyqueue.Insert(i + 1, Processes[0]);
                                Processes.RemoveAt(0);
                            }
                            else
                            {
                                readyqueue.Insert(readyqueue.Count, Processes[0]);
                                Processes.RemoveAt(0);
                            }
                        }

                    }
                }
            }
            return waiting_Time / no_Process;
        }
        bool is_FCFS = false;
        bool is_sjf = false;
        bool is_priority = false;
        bool is_RR = false;
        bool pre= false; bool non = false;
        int number_process = 0;
        List<Process> Processes = new List<Process>();
        List<double> burst_time = new List<double>();
        List<double> arrival_time = new List<double>();

        private void algorithm_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i_SelectedIndex = algorithm.SelectedIndex;
            if (i_SelectedIndex == -1)
                return;
            for (int iIndex = 0; iIndex < algorithm.Items.Count; iIndex++)
                algorithm.SetItemCheckState(iIndex, CheckState.Unchecked);
            algorithm.SetItemCheckState(i_SelectedIndex, CheckState.Checked);
            if (algorithm.SelectedIndex == 0)
            {
                is_FCFS = true;
                is_sjf = false;
                is_priority = false;
                is_RR = false;
                pre_non.Enabled = false;
                priority_text.Enabled = false;
                quantum_text.Enabled = false;
            }
            else if (algorithm.SelectedIndex == 1)
            {
                is_FCFS = false;
                is_sjf = true;
                is_priority = false;
                is_RR = false;
                pre_non.Enabled = true;
                priority_text.Enabled = false;
                quantum_text.Enabled = false;
            }
            else if (algorithm.SelectedIndex == 2)
            {
                is_FCFS = false;
                is_sjf = false;
                is_priority = true;
                is_RR = false;
                pre_non.Enabled = true;
                priority_text.Enabled = true;
                quantum_text.Enabled = false;
            }
            else if (algorithm.SelectedIndex == 3)
            {
                is_FCFS = false;
                is_sjf = false;
                is_priority = false;
                is_RR = true;
                pre_non.Enabled = false;
                priority_text.Enabled = false;
                quantum_text.Enabled = true;
            }
        }

        private void pre_non_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i_SelectedIndex = pre_non.SelectedIndex;
            if (i_SelectedIndex == -1)
                return;
            for (int iIndex = 0; iIndex < pre_non.Items.Count; iIndex++)
                pre_non.SetItemCheckState(iIndex, CheckState.Unchecked);
            pre_non.SetItemCheckState(i_SelectedIndex, CheckState.Checked);
            if (pre_non.SelectedIndex == 0)
            {
                non = true;

            }
            else if (pre_non.SelectedIndex == 1)
            {
                pre = false;

            }
        }
        private void SJF_Pree()
        {

            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
                arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(SJFP_Schedule(Processes, number_process));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (schedule[i].burst_time / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * schedule[i].burst_time) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }

        private void SJF_NP()
        {

            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
                arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(SJFNP_Schedule(Processes, number_process));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (schedule[i].burst_time / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * schedule[i].burst_time) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }
        private void FCFS()
        {
            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
                arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(FCFS_Schedule(Processes, number_process));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (schedule[i].burst_time / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * schedule[i].burst_time) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }
        private void priority_Pree()
        {
            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
                arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(PriorityP_Schedule(Processes, number_process));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (schedule[i].burst_time / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * schedule[i].burst_time) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }

        private void priority_non()
        {
            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
                arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(PriorityNP_Schedule(Processes, number_process));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (schedule[i].burst_time / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * schedule[i].burst_time) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }
        private void RoundRobin()
        {
            number_process = Convert.ToInt32(number_process_text.Text);
            double cont;
            //Process p;
            double count;
            string brust = burst_text.Text;
            string[] collection_brust = brust.Split(',');
            for (int i = 0; i < number_process; i++)
            {

                cont = Convert.ToDouble(collection_brust[i]);
                burst_time.Add(cont);
            }

            string arrival = arrival_text.Text;
            string[] collection_arrival = arrival.Split(',');
            for (int i = 0; i < number_process; i++)
            {
                cont = Convert.ToInt32(collection_arrival[i]);
       arrival_time.Add(cont);
            }
            for (int i = 0; i < number_process; i++)
            {
                string process_name = "P" + i;
                Process p = new Process();
                p.set(process_name, arrival_time[i], burst_time[i]);
                Processes.Add(p);
            }
            //double wait = FCFS_SCH(current_process, number_process);
            average_output.Text = Convert.ToString(RR_Schedule(Processes, number_process,Convert.ToInt32(quantum_text.Text)));
            int flag = 100;
            List<Label> labels = new List<Label>();
            List<Label> labels_between = new List<Label>();
            int a = 0;
            for (int i = 0; i < number_process; i++)
            {
                var temp = new Label();

                temp.Location = new Point(flag + a, 200);
                temp.Text = schedule[i].process_num;
                temp.AutoSize = false;
                temp.Size = new Size(10 * (int)Math.Round((double)(100 * (Convert.ToInt32(quantum_text.Text) / burst_time.Sum()))), 35);
                // temp.Click += new EventHandler(Form2_Load);

                temp.BackColor = System.Drawing.Color.Blue;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
                a = a + 15 + (10 * (int)Math.Round((double)(100 * Convert.ToInt32(quantum_text.Text)) / burst_time.Sum()));
                /////////
                var temp_between = new Label();

                // count += schedule[i].burst_time;
                //average_waiting.Add(count);
                temp_between.Location = new Point(flag + a - 15, 200);
                //temp_between.Text = "" + count;
                temp_between.AutoSize = false;
                temp_between.Size = new Size(15, 35);
                //temp_between.Click += new EventHandler(Form2_Load);

                temp_between.BackColor = System.Drawing.Color.Green;

                this.Controls.Add(temp_between);

                temp_between.Show();
                labels_between.Add(temp_between);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (is_FCFS)
            {
                FCFS();
            }
            else if (is_sjf && non)
            {
                SJF_NP();
            }
            else if (is_sjf && pre)
            {
                SJF_Pree();
            }
            if (is_priority && pre)
            {
                priority_Pree();
            }
            else if (is_priority & non)
            {
                priority_non();
            }
            else if (is_RR)
            {
                RoundRobin();
            }

        }
    }
}
