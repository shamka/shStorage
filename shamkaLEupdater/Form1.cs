using System;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Text;

namespace shamkaLEupdater
{
    public partial class Form1 : Form
    {
        //FORM
        public Form1()
        {
            InitializeComponent();
            clearForm(0);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (State.modified == formModState.MODIFIED)
            {
                if (MessageBox.Show("Вы уверены, что хотите закрыть и потерять изменения?",
    "Подтверждение сброса сессии",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
        private void clearForm(int c)
        {
            if (c == 0)
            {
                tabControl1.SelectedIndex = 0;
                State.Clear();
                //first tab
                btn_save.Enabled = false;
                btn_saveAs.Enabled = false;
                btn_open.Enabled = true;
                btn_new.Enabled = true;
                btn_reset.Enabled = false;
                btn_close.Enabled = false;
                le_cancel.Enabled = false;
            }
            if (c == 0 || c == 1)
            {
                //keys
                btn_delete_key.Enabled = false;
                key_name.ReadOnly = true;
                btn_export_key.Enabled = false;
                btn_apply_key.Enabled = false;
                groupBox_export.Visible = false;
                keyListUpdate();
            }
            //servers
            if (c == 0 || c == 2)
            {
                btn_delete_server.Enabled = false;
                btn_add_server.Enabled = true;
                server_name.ReadOnly = true;
                server_link.ReadOnly = true;
                server_pass.ReadOnly = true;
                server_name.Clear();
                server_link.Clear();
                server_pass.Clear();

                btn_apply_server.Enabled = false;
                serverListUpdate();
            }

            //domains
            if (c == 0 || c == 3)
            {
                btn_delete_domain.Enabled = false;
                btn_add_domain.Enabled = true;
                domain_name.ReadOnly = true;
                domain_dns.ReadOnly = true;
                domain_subs.ReadOnly = true;
                domain_name.Clear();
                domain_dns.Clear();
                domain_subs.Clear();
                subs.Clear();
                btn_apply_domain.Enabled = false;
                domainsListUpdate();
            }
            if (c == 0 || c == 4)
            {
                //certs
                btn_delete_certs.Enabled = false;
                certs_name.ReadOnly = true;
                btn_export_certs.Enabled = false;
                btn_apply_certs.Enabled = false;
                groupBox_export_certs.Visible = false;
                certsListUpdate();
            }
        }
        private void updateForm(int c)
        {
            if (c == 0)
            {
            }
            if (c == 0 || c == 1)
            {
                //keys
                keyListUpdate();
            }
            if (c == 0 || c == 2)
            {
                //server
                serverListUpdate();
            }
            //domains
            if (c == 0 || c == 3)
            {
                domainsListUpdate();
            }
            //certs
            if (c == 0 || c == 4)
            {
                certsListUpdate();
            }

        }
        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (State.state == formState.NONE)
            {
                e.Cancel = true;
            }
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0)
            {
                if (State.state != formState.NONE)
                {
                    if (State.modified == formModState.MODIFIED)
                    {
                        btn_save.Enabled = State.state == formState.FILE;
                        btn_reset.Enabled = true;
                        btn_open.Enabled = false;
                        btn_new.Enabled = false;
                    }
                    else
                    {
                        btn_save.Enabled = false;
                        btn_open.Enabled = true;
                        btn_new.Enabled = true;
                        btn_reset.Enabled = false;
                    }
                    btn_saveAs.Enabled = true;
                    if (State.state == formState.FILE)
                    {
                        btn_close.Enabled = true;
                    }
                    else
                    {
                        btn_close.Enabled = false;
                    }
                }
                else
                {
                    btn_saveAs.Enabled = false;
                    btn_save.Enabled = false;
                    btn_open.Enabled = true;
                    btn_new.Enabled = true;
                    btn_reset.Enabled = false;
                    btn_close.Enabled = false;
                }
            }
            else if (e.TabPageIndex == 4)
            {
                certsListUpdate();
            }
        }
        //BUTTONS MAIN
        private void btn_new_Click(object sender, EventArgs e)
        {
            if (State.modified == formModState.UNMODIFIED)
            {
                clearForm(0);
                State.state = formState.NEW;
                State.modified = formModState.MODIFIED;
                tabControl1.SelectTab("tab_keys");
            }
            else
            {
                MessageBox.Show("Сбросьте или сохраниете введенные данные", "Требование");
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите сбросить данные?",
                "Подтверждение сброса сессии",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clearForm(0);
            }
        }
        private void btn_saveAs_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //saveFileDialog1.Filter = "ShamkaLEupdater (*.leu)|*.leu;*.sleu|All files (*.*)|*.*";
            saveFileDialog1.Filter = "LEupdater|*.leu|All files|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        if (utils.saveStorage(myStream, State.session))
                        {
                            State.state = formState.FILE;
                            State.modified = formModState.UNMODIFIED;
                            tabControl1_Selecting(tabControl1, new TabControlCancelEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex, false, TabControlAction.Selecting));
                        }
                        else
                        {
                            throw new Exception("Ошибка при сохранении");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (utils.saveStorage(State.session))
                {
                    State.state = formState.FILE;
                    State.modified = formModState.UNMODIFIED;
                    tabControl1_Selecting(tabControl1, new TabControlCancelEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex, false, TabControlAction.Selecting));
                }
                else
                {
                    throw new Exception("Ошибка при сохранении");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btn_open_Click(object sender, EventArgs e)
        {
            if (State.modified == formModState.MODIFIED)
            {
                MessageBox.Show("Сбросьте или сохраниете введенные данные", "Требование");
                return;
            }
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.Filter = "ShamkaLEupdater (*.leu)|*.leu;*.sleu|All files (*.*)|*.*";
            openFileDialog1.Filter = "LEupdater|*.leu|All files|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    if ((myStream = File.Open(openFileDialog1.FileName, FileMode.Open)) != null)
                    {

                        State.session.Dispose();
                        State.session = utils.openStorage(myStream);
                        State.state = formState.FILE;
                        tabControl1_Selecting(tabControl1, new TabControlCancelEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex, false, TabControlAction.Selecting));
                        updateForm(0);
                    }
                }
                catch (Exception ex)
                {
                    State.session.Dispose();
                    State.session = new storageInfo();
                    clearForm(0);
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            State.state = formState.NEW;
            State.session.file = null;
            tabControl1_Selecting(tabControl1, new TabControlCancelEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex, false, TabControlAction.Selecting));
        }
        //SERVERS
        private void serverListUpdate()
        {
            cb_servers.Enabled = true;
            cb_servers.Focus();
            if (State.session.servers.Keys.Count == 0)
            {
                btn_delete_server.Enabled = false;
                server_name.ReadOnly = true;
                server_link.ReadOnly = true;
                server_pass.ReadOnly = true;
                State.selectServerName = null;
                cb_servers.DataSource = null;
                cb_servers.Items.Clear();
                le_server.DataSource = null;
                le_server.Items.Clear();
                btn_apply_server.Enabled = false;
                return;
            }
            string[] ar = new String[State.session.servers.Keys.Count];
            State.session.servers.Keys.CopyTo(ar, 0);
            Array.Sort(ar, StringComparer.InvariantCulture);
            cb_servers.DataSource = ar;
            le_server.DataSource = ar.Clone();
            if (State.selectServerName == null || !State.session.servers.ContainsKey(State.selectServerName))
            {
                State.selectServerName = ar[0];
            }
            cb_servers.SelectedItem = State.selectServerName;
            cb_servers_SelectedIndexChanged(null, null);
        }
        // BUTTONS SERVERS
        private void server_ping_Click(object sender, EventArgs e)
        {
            server_ping.Enabled = false;
            object ans;
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("m", "ping");
            try
            {
                ans = utils.toServ(server_link.Text, server_pass.Text, obj);
                MessageBox.Show((ans.GetType().Name != "string" && ((string)ans) == "pong") ? "Успех" : "Ошибка", "Результат теста");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message, "Результат теста");
                return;
            }
            finally
            {
                server_ping.Enabled = true;
            }

        }
        private void btn_add_server_Click(object sender, EventArgs e)
        {
            cb_servers.DataSource = null;
            cb_servers.Items.Clear();
            State.selectServerName = null;
            server_name.ReadOnly = false;
            server_link.ReadOnly = false;
            server_pass.ReadOnly = false;
            cb_servers.Enabled = false;
            btn_delete_server.Enabled = true;
            btn_add_server.Enabled = false;
            btn_apply_server.Enabled = false;
            server_pass.Text = "ключ сервера";
            server_link.Text = "ссылка на сервер";
            server_name.Text = "новый сервер";
        }
        private void btn_apply_server_Click(object sender, EventArgs e)
        {
            if (!le_gr.Enabled && State.selectServerName == le_server.Text)
            {
                MessageBox.Show("Объект ещё используется", "Предупреждение");
                return;
            }
            btn_apply_server.Enabled = false;
            btn_add_server.Enabled = true;
            State.modified = formModState.MODIFIED;
            if (State.selectServerName == null)
            {
                State.session.servers.Add(server_name.Text, new ServerInfo(server_link.Text, server_pass.Text));
                State.selectServerName = server_name.Text;
                serverListUpdate();
                return;
            }
            ServerInfo server = State.session.servers[State.selectServerName];
            server.link = server_link.Text;
            server.pass = server_pass.Text;
            State.session.servers.Remove(State.selectServerName);
            State.selectServerName = server_name.Text;
            State.session.servers.Add(State.selectServerName, server);
            serverListUpdate();
            cb_servers.Focus();
        }
        private void btn_delete_server_Click(object sender, EventArgs e)
        {
            if (State.selectServerName == null)
            {
                clearForm(2);
                cb_servers_SelectedIndexChanged(null, null);
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить сервер с имменем:\n" + State.selectServerName + "?",
    "Подтверждение удаления сервера",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                State.modified = formModState.MODIFIED;
                State.session.servers.Remove(State.selectServerName);
                serverListUpdate();
            }
        }
        //TEXT SERVERS
        private void server_name_TextChanged(object sender, EventArgs e)
        {
            if (State.selectServerName == null)
            {
                btn_apply_server.Enabled = !State.session.servers.ContainsKey(server_name.Text);
            }
            else
            {
                btn_apply_server.Enabled = (State.selectServerName != server_name.Text) || (server_link.Text != State.session.servers[State.selectServerName].link) || (server_pass.Text != State.session.servers[State.selectServerName].pass);
            }
        }
        private void cb_servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (State.ignoreSelectServerName)
            {
                State.ignoreSelectServerName = false;
                return;
            }
            if (cb_servers.SelectedItem == null)
            {
                server_name.Clear();
                server_link.Clear();
                server_pass.Clear();
                btn_apply_server.Enabled = false;
                cb_servers.Focus();
                return;
            }
            State.selectServerName = (string)cb_servers.SelectedItem;
            server_name.Text = State.selectServerName;
            server_link.Text = State.session.servers[State.selectServerName].link;
            server_pass.Text = State.session.servers[State.selectServerName].pass;
            btn_delete_server.Enabled = true;
            server_name.ReadOnly = false;
            server_link.ReadOnly = false;
            server_pass.ReadOnly = false;
        }
        private void cb_servers_DataSourceChanged(object sender, EventArgs e)
        {
            State.ignoreSelectServerName = true;
            btn_delete_server.Enabled = false;
            server_name.ReadOnly = true;
            server_link.ReadOnly = true;
            server_pass.ReadOnly = true;
        }
        //DOMAINS
        private void domainsListUpdate()
        {
            cb_domains.Enabled = true;
            cb_domains.Focus();
            if (State.session.domains.Keys.Count == 0)
            {
                btn_delete_domain.Enabled = false;
                domain_name.ReadOnly = true;
                domain_dns.ReadOnly = true;
                domain_subs.ReadOnly = true;
                State.selectDomainName = null;
                cb_domains.DataSource = null;
                cb_domains.Items.Clear();
                le_domain.DataSource = null;
                le_domain.Items.Clear();
                btn_apply_domain.Enabled = false;
                return;
            }
            string[] ar = new String[State.session.domains.Keys.Count];
            State.session.domains.Keys.CopyTo(ar, 0);
            Array.Sort(ar, StringComparer.InvariantCulture);
            cb_domains.DataSource = ar;
            le_domain.DataSource = ar.Clone();
            if (State.selectDomainName == null || !State.session.domains.ContainsKey(State.selectDomainName))
            {
                State.selectDomainName = ar[0];
            }
            cb_domains.SelectedItem = State.selectDomainName;
            cb_domains_SelectedIndexChanged(null, null);
        }
        private void cb_domains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (State.ignoreSelectDomainName)
            {
                State.ignoreSelectDomainName = false;
                return;
            }
            if (cb_domains.SelectedItem == null)
            {
                domain_name.Clear();
                domain_dns.Clear();
                domain_subs.Clear();
                subs.Clear();
                return;
            }
            State.selectDomainName = (string)cb_domains.SelectedItem;
            domain_name.Text = State.selectDomainName;
            domain_dns.Text = State.session.domains[State.selectDomainName].dns;
            domain_subs.Text = State.session.domains[State.selectDomainName].subs;
            btn_delete_domain.Enabled = true;
            domain_name.ReadOnly = false;
            domain_dns.ReadOnly = false;
            domain_subs.ReadOnly = false;

            string[] dmns = domain_subs.Text.Split(new char[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            subs.Text = String.Format("{0:d}", dmns.Length);


        }
        private void cb_domains_DataSourceChanged(object sender, EventArgs e)
        {
            State.ignoreSelectDomainName = true;
            btn_delete_domain.Enabled = false;
            domain_name.ReadOnly = true;
        }
        private void btn_delete_domain_Click(object sender, EventArgs e)
        {
            if (State.selectDomainName == null)
            {
                clearForm(3);
                cb_domains_SelectedIndexChanged(null, null);
                return;
            }
            if (MessageBox.Show("Вы уверены, что хотите удалить список с имменем:\n" + State.selectServerName + "?",
    "Подтверждение удаления списка",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                State.modified = formModState.MODIFIED;
                State.session.domains.Remove(State.selectDomainName);
                domainsListUpdate();
            }
        }
        private void btn_add_domain_Click(object sender, EventArgs e)
        {
            cb_domains.DataSource = null;
            cb_domains.Items.Clear();
            State.selectDomainName = null;
            domain_name.ReadOnly = false;
            domain_dns.ReadOnly = false;
            domain_subs.ReadOnly = false;
            cb_domains.Enabled = false;
            btn_delete_domain.Enabled = true;
            btn_add_domain.Enabled = false;
            btn_apply_domain.Enabled = false;
            domain_subs.Text = "@,www";
            domain_dns.Text = "example.com";
            domain_name.Text = "новый список";
            subs.Text = "2";
        }
        private void domain_name_TextChanged(object sender, EventArgs e)
        {
            if (State.selectDomainName == null)
            {
                btn_apply_domain.Enabled = !State.session.domains.ContainsKey(domain_name.Text);
            }
            else
            {
                btn_apply_domain.Enabled = (State.selectDomainName != domain_name.Text) || (domain_dns.Text != State.session.domains[State.selectDomainName].dns) || (domain_subs.Text != State.session.domains[State.selectDomainName].subs);
            }
        }
        private void btn_apply_domain_Click(object sender, EventArgs e)
        {
            if (!le_gr.Enabled && State.selectDomainName == le_domain.Text)
            {
                MessageBox.Show("Объект ещё используется", "Предупреждение");
                return;
            }

            IdnMapping idn = new IdnMapping();
            string[] dmns = domain_subs.Text.Split(new char[] { ',', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                string punyCode = idn.GetAscii(domain_dns.Text);
                Match match = Regex.Match(punyCode, @"^(?-i)[a-z0-9\.\-_]+$");
                if (!match.Success) throw new Exception("Неправильное имя домена");
                foreach (string name in dmns)
                {
                    punyCode = idn.GetAscii(name);
                    match = Regex.Match(punyCode, @"^((?-i)[a-z0-9\.\-_]+|@)$");
                    if (!match.Success) throw new Exception("Неправильное имя поддомена " + name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка");
                return;
            }

            dmns = utils.RemoveDuplicates(dmns);
            Array.Sort(dmns, StringComparer.InvariantCulture);
            domain_subs.Text = String.Join("\r\n", dmns);
            subs.Text = String.Format("{0:d}", dmns.Length);
            btn_apply_domain.Enabled = false;
            btn_add_domain.Enabled = true;
            State.modified = formModState.MODIFIED;
            if (State.selectDomainName == null)
            {
                State.session.domains.Add(domain_name.Text, new DomainInfo(domain_dns.Text, domain_subs.Text));
                State.selectDomainName = domain_name.Text;
                domainsListUpdate();
                return;
            }
            DomainInfo domain = State.session.domains[State.selectDomainName];
            domain.dns = domain_dns.Text;
            domain.subs = domain_subs.Text;
            State.session.domains.Remove(State.selectDomainName);
            State.selectDomainName = domain_name.Text;
            State.session.domains.Add(State.selectDomainName, domain);
            domainsListUpdate();
            cb_domains.Focus();
        }
        //BUTTONS KEYS
        private void btn_add_key_Click(object sender, EventArgs e)
        {
            //this.Enabled=false;
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.Filter = "ShamkaLEupdater (*.leu)|*.leu;*.sleu|All files (*.*)|*.*";
            openFileDialog1.Filter = "Private keys|*.openssl;*.key;*.pkcs8|All files|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {

                            keyInfo key = utils.parsePrivKey(myStream);
                            foreach (var pair in State.session.keys)
                            {
                                if (pair.Value.pinSHA256 == key.pinSHA256)
                                {
                                    cb_keys.SelectedItem = pair.Key;
                                    throw new Exception("Ключ уже добавлен");
                                }
                            }
                            State.selectKeyName = String.Format("*{0:d}***{1:s}***{2:s}", key.bits, openFileDialog1.SafeFileName, key.pinSHA256);
                            State.session.keys.Add(State.selectKeyName, key);
                            State.modified = formModState.MODIFIED;
                            keyListUpdate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            //this.Enabled = true;
        }
        private void btn_export_key_Click(object sender, EventArgs e)
        {
            btn_export_key.Enabled = false;
            groupBox_export.Visible = true;
            groupBox_export.Focus();
        }
        private void btn_delete_key_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить ключ с имменем:\n" + State.selectKeyName + "?",
                "Подтверждение удаления ключа",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                State.modified = formModState.MODIFIED;
                State.session.keys.Remove(State.selectKeyName);
                keyListUpdate();
            }
        }
        private void btn_apply_key_Click(object sender, EventArgs e)
        {
            if (!le_gr.Enabled && (State.selectKeyName == le_privKey.Text || State.selectKeyName == le_userKey.Text))
            {
                MessageBox.Show("Объект ещё используется", "Предупреждение");
                return;
            }

            btn_apply_key.Enabled = false;
            keyInfo key = State.session.keys[State.selectKeyName];
            State.session.keys.Remove(State.selectKeyName);
            State.selectKeyName = key_name.Text;
            State.session.keys.Add(State.selectKeyName, key);
            State.modified = formModState.MODIFIED;
            keyListUpdate();
            groupBox_keys.Focus();
        }
        //TEXTNAME KEYS
        private void key_name_TextChanged(object sender, EventArgs e)
        {
            btn_apply_key.Enabled = ((State.selectKeyName != null) && (State.selectKeyName != key_name.Text)) ? !State.session.keys.ContainsKey(key_name.Text) : false;
        }
        //COMBOBOX KEYS
        private void cb_keys_DataSourceChanged(object sender, EventArgs e)
        {
            State.ignoreSelectKeyName = true;
            btn_delete_key.Enabled = false;
            key_name.ReadOnly = true;
            btn_export_key.Enabled = false;
        }
        private void cb_keys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (State.ignoreSelectKeyName)
            {
                State.ignoreSelectKeyName = false;
                return;
            }
            if (cb_keys.SelectedItem == null)
            {
                key_name.Clear();
                keyLength.Clear();
                keyPin.Clear();
                groupBox_keys.Focus();
                return;
            }
            State.selectKeyName = (string)cb_keys.SelectedItem;
            key_name.Text = State.selectKeyName;
            keyLength.Text = State.session.keys[State.selectKeyName].bits.ToString();
            keyPin.Text = State.session.keys[State.selectKeyName].pinSHA256;
            btn_delete_key.Enabled = true;
            key_name.ReadOnly = false;
            btn_export_key.Enabled = true;
            groupBox_export.Visible = false;
        }
        //UPDATE KEYS
        private void keyListUpdate()
        {
            State.session.keyPins.Clear();
            if (State.session.keys.Keys.Count == 0)
            {
                btn_delete_key.Enabled = false;
                key_name.ReadOnly = true;
                btn_export_key.Enabled = false;
                btn_apply_key.Enabled = false;
                groupBox_export.Visible = false;
                State.selectKeyName = null;
                cb_keys.DataSource = null;
                cb_keys.Items.Clear();
                le_privKey.DataSource = null;
                le_privKey.Items.Clear();
                le_userKey.DataSource = null;
                le_userKey.Items.Clear();
                return;
            }
            string[] ar = new String[State.session.keys.Keys.Count];
            State.session.keys.Keys.CopyTo(ar, 0);
            Array.Sort(ar, StringComparer.InvariantCulture);
            foreach(string keyName in ar) {
                State.session.keyPins.Add(State.session.keys[keyName].pinSHA256);
            }
            cb_keys.DataSource = ar;
            List<string> usKeys = new List<string>();
            foreach (string userKeyName in ar) {
                Match match = Regex.Match(userKeyName, @"\ \*u$");
                if (!match.Success) continue;
                usKeys.Add(userKeyName);
            }
            le_userKey.DataSource = usKeys;
            le_privKey.DataSource = ar.Clone();
            if (State.selectKeyName == null || !State.session.keys.ContainsKey(State.selectKeyName))
            {
                State.selectKeyName = ar[0];
            }
            cb_keys.SelectedItem = State.selectKeyName;
            cb_keys_SelectedIndexChanged(null, null);
        }
        //CERTS
        private void certs_name_TextChanged(object sender, EventArgs e)
        {
            btn_apply_certs.Enabled = ((State.selectCertName != null) && (State.selectCertName != certs_name.Text)) ? !State.session.certs.ContainsKey(certs_name.Text) : false;
        }
        private void certsListUpdate()
        {
            if (State.session.certs.Keys.Count == 0)
            {
                btn_delete_certs.Enabled = false;
                certs_name.ReadOnly = true;
                btn_export_certs.Enabled = false;
                btn_apply_certs.Enabled = false;
                groupBox_export_certs.Visible = false;
                State.selectCertName = null;
                cb_certs.DataSource = null;
                cb_certs.Items.Clear();
                return;
            }
            string[] ar = new String[State.session.certs.Keys.Count];
            State.session.certs.Keys.CopyTo(ar, 0);
            Array.Sort(ar, StringComparer.InvariantCulture);
            cb_certs.DataSource = ar;
            if (State.selectCertName == null || !State.session.certs.ContainsKey(State.selectCertName))
            {
                State.selectCertName = ar[0];
            }
            cb_certs.SelectedItem = State.selectCertName;
            cb_certs_SelectedIndexChanged(null, null);
        }
        private void btn_add_certs_Click(object sender, EventArgs e)

        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Public keys|*.pem;*.cer;*.crt;*.der|All files|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {

                            certInfo cert = utils.parsePubKey(myStream);
                            foreach (var pair in State.session.certs)
                            {
                                if (pair.Value.fingerPrint == cert.fingerPrint)
                                {
                                    cb_certs.SelectedItem = pair.Key;
                                    throw new Exception("Сертификат уже добавлен");
                                }
                            }
                            State.selectCertName = String.Format("*{0:d}***{1:s}***{2:s}", cert.bits, openFileDialog1.SafeFileName, cert.fingerPrint);
                            State.session.certs.Add(State.selectCertName, cert);
                            State.modified = formModState.MODIFIED;
                            certsListUpdate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            //this.Enabled = true;
        }
        private void cb_certs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (State.ignoreSelectCertName)
            {
                State.ignoreSelectCertName = false;
                return;
            }
            if (cb_certs.SelectedItem == null)
            {
                certs_name.Clear();
                certs_length.Clear();
                certs_pin.Clear();
                certs_finger.Clear();
                certs_cns.Clear();
                groupBox_certs.Focus();
                return;
            }
            State.selectCertName = (string)cb_certs.SelectedItem;
            certs_name.Text = State.selectCertName;
            certs_pin.Text = State.session.certs[State.selectCertName].pinSHA256;
            certs_length.Text = State.session.certs[State.selectCertName].bits.ToString() +
                ((State.session.keyPins.Contains(certs_pin.Text)) ? " (P)" : "") +
                ((State.session.certs[State.selectCertName].CN == State.session.certs[State.selectCertName].iCN) ? " (ROOT)" : "");
            certs_cns.Text = State.session.certs[State.selectCertName].CN + ((State.session.certs[State.selectCertName].CN != State.session.certs[State.selectCertName].iCN) ? " <> " + State.session.certs[State.selectCertName].iCN : "");
            certs_finger.Text = State.session.certs[State.selectCertName].fingerPrint;
            btn_delete_certs.Enabled = true;
            certs_name.ReadOnly = false;
            btn_export_certs.Enabled = true;
            groupBox_export_certs.Visible = false;
        }
        private void btn_export_certs_Click(object sender, EventArgs e)
        {
            btn_export_certs.Enabled = false;
            groupBox_export_certs.Visible = true;
            groupBox_export_certs.Focus();
        }
        private void btn_delete_certs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить сертификат с имменем:\n" + State.selectCertName + "?",
    "Подтверждение удаления сертификата",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                State.modified = formModState.MODIFIED;
                State.session.certs.Remove(State.selectCertName);
                certsListUpdate();
            }
        }
        private void btn_apply_certs_Click(object sender, EventArgs e)
        {
            btn_apply_certs.Enabled = false;
            certInfo cert = State.session.certs[State.selectCertName];
            State.session.certs.Remove(State.selectCertName);
            State.selectCertName = certs_name.Text;
            State.session.certs.Add(State.selectCertName, cert);
            State.modified = formModState.MODIFIED;
            certsListUpdate();
            groupBox_certs.Focus();
        }
        private void cb_certs_DataSourceChanged(object sender, EventArgs e)
        {
            State.ignoreSelectCertName = true;
            btn_delete_certs.Enabled = false;
            certs_name.ReadOnly = true;
            btn_export_certs.Enabled = false;
        }
        private void btn_export_openssl_Click(object sender, EventArgs e)
        {
            if (State.selectKeyName == null) return;
            if (!State.session.keys.ContainsKey(State.selectKeyName)) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "openssl|*.openssl|key|*.key|pem|*.pem|All files|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.Write(utils.dataTo64(State.session.keys[State.selectKeyName].key.makeDer(), "RSA PRIVATE KEY"));
                writer.Dispose();
                writer.Close();
            }
        }
        private void btn_certs_cer_Click(object sender, EventArgs e)
        {
            if (State.selectCertName == null) return;
            if (!State.session.certs.ContainsKey(State.selectCertName)) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Certificate|*.cer|All files|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.Write(utils.dataTo64(State.session.certs[State.selectCertName].cert.makeDer(), "CERTIFICATE"));
                writer.Dispose();
                writer.Close();
            }
        }
        private void btn_certs_cer_chain_Click(object sender, EventArgs e)
        {
            if (State.selectCertName == null) return;
            if (!State.session.certs.ContainsKey(State.selectCertName)) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Certificate|*.cer|All files|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.Write(utils.dataTo64(State.session.certs[State.selectCertName].cert.makeDer(), "CERTIFICATE"));

                if (State.session.certs[State.selectCertName].CN != State.session.certs[State.selectCertName].iCN) {
                    certInfo current = State.session.certs[State.selectCertName];
                    Looper1:
                    foreach (var cert in State.session.certs) {
                        if (cert.Value.CN != current.iCN) continue;
                        current = cert.Value;
                        if (current.CN == current.iCN) break;
                        writer.Write(utils.dataTo64(current.cert.makeDer(), "CERTIFICATE"));
                        goto Looper1;
                    }
                }
                writer.Dispose();
                writer.Close();
            }
        }

        private void btn_export_keycer_Click(object sender, EventArgs e)
        {
            if (State.selectKeyName == null) return;
            if (!State.session.keys.ContainsKey(State.selectKeyName)) return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Cert|*.cer|All files|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.Write(utils.dataTo64(utils.makeRootCertFromPriv(State.selectKeyName), "CERTIFICATE"));
                writer.Dispose();
                writer.Close();
            }
        }

        //LE
        private void le_reg_Click(object sender, EventArgs e)
        {
            if (le_domain.Text == "") return;
            le_gr.Enabled = false;
            string met = "new-reg";
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("resource", met);
            data.Add("contact", "mailto:webmaster@" + State.session.domains[le_domain.Text].dns);
            data.Add("agreement", LE.pdf);
            le_backgr.RunWorkerAsync(new object[] { 0, met, data });
        }
        private void le_domain_SelectedIndexChanged(object sender, EventArgs e)
        {
            State.le.csr = null;
            if (State.session.domains.ContainsKey(le_domain.Text))
                le_defDomain.DataSource = State.session.domains[le_domain.Text].subs2;
            
        }
        private void le_makeCSR_Click(object sender, EventArgs e)
        {
            le_gr.Enabled = false;
            le_backgr.RunWorkerAsync(new object[] { 1, le_privKey.Text, le_defDomain.Text, le_domain.Text });
        }
        private void le_userKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!State.session.keys.ContainsKey(le_userKey.Text)) {
                State.le.Dispose();
            }
            else
            {
                State.le.setKey(State.session.keys[le_userKey.Text]);
            }
        }
        private void le_backgr_DoWork(object sender, DoWorkEventArgs e)
        {
            
            object[] args = (object[])e.Argument;
            e.Result = null;
            try
            {
                switch ((int)args[0])
                {
                    case 0:
                        e.Result = new object[] { 0, LE.makeReq((string)args[1], args[2], le_backgr, false) };
                        break;
                    case 1:
                        State.le.csr = utils.makeCSR(State.session.keys[(string)args[1]], (string)args[2], State.session.domains[(string)args[3]], le_backgr);
                        le_backgr.ReportProgress(101, new object[] { -2, "OK" });
                        e.Result = new object[] { 1 };
                        break;
                    case 2:
                        int i = 0;
                        string[] dms = State.session.domains[(string)args[1]].subs2;
                        string domain = State.session.domains[(string)args[1]].dns;
                        le_backgr.ReportProgress(1);
                        le_backgr.ReportProgress(101, new object[] { -2, String.Format("Начало теста доменов {1:s}. Всего: {0:d}", dms.Length, (string)args[1]) });
                        foreach (string sub in dms) {
                            if (le_backgr != null) if (le_backgr.CancellationPending)
                            {
                                le_backgr.ReportProgress(101, new object[] { -2, "CANCEL" });
                                le_backgr.ReportProgress(100);
                                return;
                            }
                            string test = null;
                            i++;
                            if (sub == "@") {
                                test = domain;
                            }
                            else {
                                test = sub + "." + domain;
                            }
                            le_backgr.ReportProgress(101, new object[] { -3, String.Format("{0:d}/{2:d} [ {1:s} ] send..", i, test, dms.Length) });
                            string met = "new-authz";
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("resource", met);
                            data.Add("identifier", new Dictionary<string, object>());
                            ((Dictionary<string, object>)(data["identifier"])).Add("type", "dns");
                            ((Dictionary<string, object>)(data["identifier"])).Add("value", test);
                            JObject ans = (JObject)LE.makeReq(met, data, null, false);
                            if (ans==null || LE.ME.code != 201) {
                                le_backgr.ReportProgress(101, new object[] { -2, String.Format("ERROR") });
                                le_backgr.ReportProgress(100);
                                e.Result = null;
                                return;
                            }
                            le_backgr.ReportProgress(101, new object[] { -3, String.Format("make..") });
                            string uri = null, token = null, status = ans["status"].ToObject<string>();
                            if (status == "pending")
                            {
                                foreach (Dictionary<string, string> val in ans["challenges"].ToObject<List<Dictionary<string, string>>>())
                                {
                                    if (val["type"] != "http-01") continue;
                                    uri = val["uri"];
                                    token = val["token"];
                                    break;
                                }
                                if (uri == null || token == null)
                                {
                                    le_backgr.ReportProgress(101, new object[] { -2, String.Format("ERROR") });
                                    le_backgr.ReportProgress(100);
                                    e.Result = null;
                                    return;
                                }
                                try
                                {
                                    Dictionary<string, string> dd = new Dictionary<string, string>();
                                    dd.Add("m", "add");
                                    dd.Add("id", token);
                                    dd.Add("val", token + "." + LE.th);
                                    object ans2 = utils.toServ(State.session.servers[(string)args[2]].link, State.session.servers[(string)args[2]].pass, dd);
                                }
                                catch (Exception ex)
                                {
                                    le_backgr.ReportProgress(101, new object[] { -2, String.Format("ERROR") });
                                    le_backgr.ReportProgress(100);
                                    e.Result = null;
                                    MessageBox.Show("Ошибка\n" + ex.Message, "Результат");
                                    return;
                                }
                                le_backgr.ReportProgress(101, new object[] { -3, String.Format("pending..") });
                                data.Clear();
                                data.Add("resource", "challenge");
                                data.Add("keyAuthorization", token + "." + LE.th);
                                ans = (JObject)LE.makeReq(uri, data, null, false);
                                le_backgr.ReportProgress(101, new object[] { -3, String.Format("wait..") });
                                while (true) {
                                    ans = (JObject)LE.GET(uri, null, false);
                                    if (ans["status"].ToObject<string>() == "valid") break;
                                    if (ans["status"].ToObject<string>() == "invalid") {
                                        le_backgr.ReportProgress(101, new object[] { -2, "INVALID" });
                                        le_backgr.ReportProgress(100);
                                        return;
                                    }
                                    if (le_backgr != null) if (le_backgr.CancellationPending)
                                    {
                                        le_backgr.ReportProgress(101, new object[] { -2, "CANCEL" });
                                        le_backgr.ReportProgress(100);
                                        return;
                                    }
                                    Thread.Sleep(3000);
                                }
                                le_backgr.ReportProgress(100);
                            }
                            else {

                            }
                            le_backgr.ReportProgress(101, new object[] { -2, String.Format("OK") });
                            le_backgr.ReportProgress(100);
                        }
                        e.Result = new object[] { 2 };
                        break;
                    case 3:
                        le_backgr.ReportProgress(101, new object[] { -2, "GET_CERT START" });
                        string met_3 = "new-cert";
                        Dictionary<string, object> data_3 = new Dictionary<string, object>();
                        data_3.Add("resource", met_3);
                        data_3.Add("csr", Convert.ToBase64String(State.le.csr).TrimEnd('=').Replace('+', '-').Replace('/', '_'));
                        byte[] ans_3 = (byte[])LE.makeReq(met_3, data_3, le_backgr, true);
                        if (ans_3 != null)
                        {
                            if (ans_3[0] != 0x30) throw new Exception("Ошибка в ASN1 данных");
                            Libraries.Ber c = new Libraries.Ber(ans_3);
                            if (c.childs.Count != 3) { throw new Exception("Не удалось обнаружить сертификат"); }
                            certInfo cert = new certInfo(c);
                            foreach (var pair in State.session.certs)
                            {
                                if (pair.Value.fingerPrint == cert.fingerPrint)
                                {
                                    cb_certs.SelectedItem = pair.Key;
                                    le_backgr.ReportProgress(101, new object[] { -2, "GET_CERT ERROR" });
                                    throw new Exception("Сертификат уже добавлен");
                                }
                            }
                            State.selectCertName = String.Format("*{0:d}***{2:s}***{1:s}", cert.bits, cert.fingerPrint, (string)args[1]);
                            State.session.certs.Add(State.selectCertName, cert);
                            State.modified = formModState.MODIFIED;
                            le_backgr.ReportProgress(101, new object[] { -2, "GET_CERT OK" });
                            e.Result = new object[] { 3 };
                        }
                        else {
                            le_backgr.ReportProgress(101, new object[] { -2, "GET_CERT ERROR" });
                            if(LE.ME.details!=null) le_backgr.ReportProgress(101, new object[] { -1, LE.ME.details });
                        }
                        break;
                }

            }
            catch (Exception ex) {
                e.Result = null;
                le_backgr.ReportProgress(101, new object[] { -1, ex.Message });
                return;
            }
        }   
        private void le_backgr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            le_gr.Enabled = true;
            if (e.Result == null) return;
            switch ((int)((object[])e.Result)[0]) {
                case 0:
                    break;
                case 1:
                    le_log.AppendText("CSR updated and copy to clipcboard!\r\n");
                    Clipboard.SetText(utils.dataTo64(State.le.csr, "CERTIFICATE REQUEST"));
                    break;
                case 2:
                    le_log.AppendText("Domains test Copmpleted\r\n");
                    break;
                case 3:
                    tabControl1.SelectTab("tab_certs");
                    certsListUpdate();
                    break;
            }
        }
        private void le_backgr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1) {
                le_cancel.Enabled = true;
                return;
            }
            else if (e.ProgressPercentage == 100)
            {
                le_cancel.Enabled = false;
            }
            else if (e.ProgressPercentage == 101)
            {
                switch ((int)((object[])e.UserState)[0]) {
                    case -2:
                        le_log.AppendText((string)((object[])e.UserState)[1] + "\r\n");
                        break;
                    case -3:
                        le_log.AppendText((string)((object[])e.UserState)[1]);
                        break;
                    case -1:
                        MessageBox.Show((string)((object[])e.UserState)[1], "Ошибка LE");
                        break;
                }
            }
            
        }
        private void le_defDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            State.le.csr = null;
        }

        private void le_privKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            State.le.csr = null;
        }

        private void le_cancel_Click(object sender, EventArgs e)
        {
            le_cancel.Enabled = false;
            le_backgr.CancelAsync();
        }

        private void le_clear_Click(object sender, EventArgs e)
        {
            le_log.Clear();
        }

        private void le_dTest_Click(object sender, EventArgs e)
        {
            le_gr.Enabled = false;
            le_backgr.RunWorkerAsync(new object[] { 2, le_domain.Text, le_server.Text });
        }

        private void le_getCERT_Click(object sender, EventArgs e)
        {
            le_gr.Enabled = false;
            if (State.le.csr == null) {
                le_log.AppendText("CSR is NULL\r\n");
                le_gr.Enabled = true;
                return;
            }
            le_backgr.RunWorkerAsync(new object[] { 3, le_domain.Text });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (State.selectKeyName == null) return;
            if (!State.session.keys.ContainsKey(State.selectKeyName)) return;
            string f = "C:\\Users\\shamka\\Desktop\\SSL\\MAIN\\mojang.com.cer";
            FileStream r = File.Open(f, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                Libraries.Ber cert = utils.parsePubKey(r).cert;
                Libraries.Ber s = cert.c[0].c[7].c[0].c[0].c[1].c[0];
                s.delAllChild();
                cert.childs[0].childs[4].childs[0].payload = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString(@"yyMMddhhmmssZ"));
                cert.childs[0].childs[4].childs[1].payload = Encoding.UTF8.GetBytes(DateTime.UtcNow.AddDays(3650).ToString(@"yyMMddhhmmssZ"));
                cert.childs[0].childs[1].payload = BitConverter.GetBytes(DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                s.addChild(new Libraries.Ber(Libraries.BerClass.CONTEXT, Libraries.BerTags.INTEGER, false, Encoding.UTF8.GetBytes("minecraft.net")));
                s.addChild(new Libraries.Ber(Libraries.BerClass.CONTEXT, Libraries.BerTags.INTEGER, false, Encoding.UTF8.GetBytes("mojang.com")));
                s.addChild(new Libraries.Ber(Libraries.BerClass.CONTEXT, Libraries.BerTags.INTEGER, false, Encoding.UTF8.GetBytes("*.minecraft.net")));
                s.addChild(new Libraries.Ber(Libraries.BerClass.CONTEXT, Libraries.BerTags.INTEGER, false, Encoding.UTF8.GetBytes("*.realms.minecraft.net")));
                s.addChild(new Libraries.Ber(Libraries.BerClass.CONTEXT, Libraries.BerTags.INTEGER, false, Encoding.UTF8.GetBytes("*.mojang.com")));

                keyInfo key = State.session.keys[State.selectKeyName];
                cert.childs[2].payload = utils.makeSign(key, cert.childs[0].makeDer());
                r.Seek(0, SeekOrigin.Begin);
                byte[] m = System.Text.Encoding.UTF8.GetBytes(utils.dataTo64(cert.makeDer(), "CERTIFICATE"));

                r.SetLength(m.Length);
                r.Write(m,0,m.Length);
                r.Close();
            }
            catch (Exception tt) {

            }
            
        }
    }
}

