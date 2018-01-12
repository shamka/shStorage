namespace shamkaLEupdater
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab_certs = new System.Windows.Forms.TabPage();
            this.groupBox_certs = new System.Windows.Forms.GroupBox();
            this.certs_cns = new System.Windows.Forms.TextBox();
            this.certs_finger = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox_export_certs = new System.Windows.Forms.GroupBox();
            this.btn_certs_cer_chain = new System.Windows.Forms.Button();
            this.btn_certs_cer = new System.Windows.Forms.Button();
            this.certs_length = new System.Windows.Forms.TextBox();
            this.certs_pin = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_apply_certs = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.certs_name = new System.Windows.Forms.TextBox();
            this.btn_delete_certs = new System.Windows.Forms.Button();
            this.btn_export_certs = new System.Windows.Forms.Button();
            this.btn_add_certs = new System.Windows.Forms.Button();
            this.cb_certs = new System.Windows.Forms.ComboBox();
            this.tab_servers = new System.Windows.Forms.TabPage();
            this.server_ping = new System.Windows.Forms.Button();
            this.server_pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_apply_server = new System.Windows.Forms.Button();
            this.server_link = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.server_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_delete_server = new System.Windows.Forms.Button();
            this.btn_add_server = new System.Windows.Forms.Button();
            this.cb_servers = new System.Windows.Forms.ComboBox();
            this.tab_domains = new System.Windows.Forms.TabPage();
            this.subs = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.domain_subs = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.domain_dns = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.domain_name = new System.Windows.Forms.TextBox();
            this.btn_apply_domain = new System.Windows.Forms.Button();
            this.btn_delete_domain = new System.Windows.Forms.Button();
            this.btn_add_domain = new System.Windows.Forms.Button();
            this.cb_domains = new System.Windows.Forms.ComboBox();
            this.tab_keys = new System.Windows.Forms.TabPage();
            this.groupBox_keys = new System.Windows.Forms.GroupBox();
            this.groupBox_export = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_export_keycer = new System.Windows.Forms.Button();
            this.btn_export_openssl = new System.Windows.Forms.Button();
            this.keyLength = new System.Windows.Forms.TextBox();
            this.keyPin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_apply_key = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.key_name = new System.Windows.Forms.TextBox();
            this.btn_delete_key = new System.Windows.Forms.Button();
            this.btn_export_key = new System.Windows.Forms.Button();
            this.btn_add_key = new System.Windows.Forms.Button();
            this.cb_keys = new System.Windows.Forms.ComboBox();
            this.tab_begin = new System.Windows.Forms.TabPage();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_saveAs = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_le = new System.Windows.Forms.TabPage();
            this.le_clear = new System.Windows.Forms.Button();
            this.le_cancel = new System.Windows.Forms.Button();
            this.le_gr = new System.Windows.Forms.GroupBox();
            this.le_privKey = new System.Windows.Forms.ComboBox();
            this.le_server = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.le_userKey = new System.Windows.Forms.ComboBox();
            this.le_getCERT = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.le_makeCSR = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.le_dTest = new System.Windows.Forms.Button();
            this.le_domain = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.le_reg = new System.Windows.Forms.Button();
            this.le_defDomain = new System.Windows.Forms.ComboBox();
            this.le_log = new System.Windows.Forms.TextBox();
            this.le_backgr = new System.ComponentModel.BackgroundWorker();
            this.tExpired = new System.Windows.Forms.TextBox();
            this.tab_certs.SuspendLayout();
            this.groupBox_certs.SuspendLayout();
            this.groupBox_export_certs.SuspendLayout();
            this.tab_servers.SuspendLayout();
            this.tab_domains.SuspendLayout();
            this.tab_keys.SuspendLayout();
            this.groupBox_keys.SuspendLayout();
            this.groupBox_export.SuspendLayout();
            this.tab_begin.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_le.SuspendLayout();
            this.le_gr.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_certs
            // 
            this.tab_certs.Controls.Add(this.groupBox_certs);
            this.tab_certs.Location = new System.Drawing.Point(4, 25);
            this.tab_certs.Name = "tab_certs";
            this.tab_certs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_certs.Size = new System.Drawing.Size(630, 325);
            this.tab_certs.TabIndex = 5;
            this.tab_certs.Text = "Сертификаты";
            this.tab_certs.UseVisualStyleBackColor = true;
            // 
            // groupBox_certs
            // 
            this.groupBox_certs.Controls.Add(this.tExpired);
            this.groupBox_certs.Controls.Add(this.certs_cns);
            this.groupBox_certs.Controls.Add(this.certs_finger);
            this.groupBox_certs.Controls.Add(this.label15);
            this.groupBox_certs.Controls.Add(this.groupBox_export_certs);
            this.groupBox_certs.Controls.Add(this.certs_length);
            this.groupBox_certs.Controls.Add(this.certs_pin);
            this.groupBox_certs.Controls.Add(this.label11);
            this.groupBox_certs.Controls.Add(this.btn_apply_certs);
            this.groupBox_certs.Controls.Add(this.label12);
            this.groupBox_certs.Controls.Add(this.label13);
            this.groupBox_certs.Controls.Add(this.certs_name);
            this.groupBox_certs.Controls.Add(this.btn_delete_certs);
            this.groupBox_certs.Controls.Add(this.btn_export_certs);
            this.groupBox_certs.Controls.Add(this.btn_add_certs);
            this.groupBox_certs.Controls.Add(this.cb_certs);
            this.groupBox_certs.Location = new System.Drawing.Point(0, 0);
            this.groupBox_certs.Name = "groupBox_certs";
            this.groupBox_certs.Size = new System.Drawing.Size(630, 328);
            this.groupBox_certs.TabIndex = 13;
            this.groupBox_certs.TabStop = false;
            // 
            // certs_cns
            // 
            this.certs_cns.Location = new System.Drawing.Point(222, 104);
            this.certs_cns.Name = "certs_cns";
            this.certs_cns.ReadOnly = true;
            this.certs_cns.Size = new System.Drawing.Size(400, 20);
            this.certs_cns.TabIndex = 13;
            // 
            // certs_finger
            // 
            this.certs_finger.Location = new System.Drawing.Point(116, 158);
            this.certs_finger.Name = "certs_finger";
            this.certs_finger.ReadOnly = true;
            this.certs_finger.Size = new System.Drawing.Size(323, 20);
            this.certs_finger.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 161);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Отпечаток";
            // 
            // groupBox_export_certs
            // 
            this.groupBox_export_certs.Controls.Add(this.btn_certs_cer_chain);
            this.groupBox_export_certs.Controls.Add(this.btn_certs_cer);
            this.groupBox_export_certs.Location = new System.Drawing.Point(5, 186);
            this.groupBox_export_certs.Name = "groupBox_export_certs";
            this.groupBox_export_certs.Size = new System.Drawing.Size(622, 139);
            this.groupBox_export_certs.TabIndex = 8;
            this.groupBox_export_certs.TabStop = false;
            this.groupBox_export_certs.Text = "Экспортирование";
            // 
            // btn_certs_cer_chain
            // 
            this.btn_certs_cer_chain.Location = new System.Drawing.Point(84, 19);
            this.btn_certs_cer_chain.Name = "btn_certs_cer_chain";
            this.btn_certs_cer_chain.Size = new System.Drawing.Size(90, 23);
            this.btn_certs_cer_chain.TabIndex = 1;
            this.btn_certs_cer_chain.Text = "CER chain";
            this.btn_certs_cer_chain.UseVisualStyleBackColor = true;
            this.btn_certs_cer_chain.Click += new System.EventHandler(this.btn_certs_cer_chain_Click);
            // 
            // btn_certs_cer
            // 
            this.btn_certs_cer.Location = new System.Drawing.Point(3, 19);
            this.btn_certs_cer.Name = "btn_certs_cer";
            this.btn_certs_cer.Size = new System.Drawing.Size(75, 23);
            this.btn_certs_cer.TabIndex = 0;
            this.btn_certs_cer.Text = "CER";
            this.btn_certs_cer.UseVisualStyleBackColor = true;
            this.btn_certs_cer.Click += new System.EventHandler(this.btn_certs_cer_Click);
            // 
            // certs_length
            // 
            this.certs_length.Location = new System.Drawing.Point(116, 104);
            this.certs_length.Name = "certs_length";
            this.certs_length.ReadOnly = true;
            this.certs_length.Size = new System.Drawing.Size(100, 20);
            this.certs_length.TabIndex = 6;
            // 
            // certs_pin
            // 
            this.certs_pin.Location = new System.Drawing.Point(116, 129);
            this.certs_pin.Name = "certs_pin";
            this.certs_pin.ReadOnly = true;
            this.certs_pin.Size = new System.Drawing.Size(323, 20);
            this.certs_pin.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "sha256-pinning";
            // 
            // btn_apply_certs
            // 
            this.btn_apply_certs.Location = new System.Drawing.Point(357, 43);
            this.btn_apply_certs.Name = "btn_apply_certs";
            this.btn_apply_certs.Size = new System.Drawing.Size(119, 23);
            this.btn_apply_certs.TabIndex = 4;
            this.btn_apply_certs.Text = "Применить";
            this.btn_apply_certs.UseVisualStyleBackColor = true;
            this.btn_apply_certs.Click += new System.EventHandler(this.btn_apply_certs_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Свойства ключа";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Имя ключа";
            // 
            // certs_name
            // 
            this.certs_name.Location = new System.Drawing.Point(116, 72);
            this.certs_name.Name = "certs_name";
            this.certs_name.ReadOnly = true;
            this.certs_name.Size = new System.Drawing.Size(506, 20);
            this.certs_name.TabIndex = 5;
            this.certs_name.TextChanged += new System.EventHandler(this.certs_name_TextChanged);
            // 
            // btn_delete_certs
            // 
            this.btn_delete_certs.Location = new System.Drawing.Point(237, 43);
            this.btn_delete_certs.Name = "btn_delete_certs";
            this.btn_delete_certs.Size = new System.Drawing.Size(114, 23);
            this.btn_delete_certs.TabIndex = 3;
            this.btn_delete_certs.Text = "Удалить ключ";
            this.btn_delete_certs.UseVisualStyleBackColor = true;
            this.btn_delete_certs.Click += new System.EventHandler(this.btn_delete_certs_Click);
            // 
            // btn_export_certs
            // 
            this.btn_export_certs.Location = new System.Drawing.Point(125, 43);
            this.btn_export_certs.Name = "btn_export_certs";
            this.btn_export_certs.Size = new System.Drawing.Size(106, 23);
            this.btn_export_certs.TabIndex = 2;
            this.btn_export_certs.Text = "Экспорт ключа";
            this.btn_export_certs.UseVisualStyleBackColor = true;
            this.btn_export_certs.Click += new System.EventHandler(this.btn_export_certs_Click);
            // 
            // btn_add_certs
            // 
            this.btn_add_certs.Location = new System.Drawing.Point(10, 43);
            this.btn_add_certs.Name = "btn_add_certs";
            this.btn_add_certs.Size = new System.Drawing.Size(109, 23);
            this.btn_add_certs.TabIndex = 1;
            this.btn_add_certs.Text = "Добавить ключ";
            this.btn_add_certs.UseVisualStyleBackColor = true;
            this.btn_add_certs.Click += new System.EventHandler(this.btn_add_certs_Click);
            // 
            // cb_certs
            // 
            this.cb_certs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_certs.FormattingEnabled = true;
            this.cb_certs.Location = new System.Drawing.Point(5, 16);
            this.cb_certs.Name = "cb_certs";
            this.cb_certs.Size = new System.Drawing.Size(617, 21);
            this.cb_certs.TabIndex = 0;
            this.cb_certs.SelectedIndexChanged += new System.EventHandler(this.cb_certs_SelectedIndexChanged);
            this.cb_certs.DataSourceChanged += new System.EventHandler(this.cb_certs_DataSourceChanged);
            // 
            // tab_servers
            // 
            this.tab_servers.Controls.Add(this.server_ping);
            this.tab_servers.Controls.Add(this.server_pass);
            this.tab_servers.Controls.Add(this.label6);
            this.tab_servers.Controls.Add(this.btn_apply_server);
            this.tab_servers.Controls.Add(this.server_link);
            this.tab_servers.Controls.Add(this.label5);
            this.tab_servers.Controls.Add(this.server_name);
            this.tab_servers.Controls.Add(this.label4);
            this.tab_servers.Controls.Add(this.btn_delete_server);
            this.tab_servers.Controls.Add(this.btn_add_server);
            this.tab_servers.Controls.Add(this.cb_servers);
            this.tab_servers.Location = new System.Drawing.Point(4, 25);
            this.tab_servers.Name = "tab_servers";
            this.tab_servers.Padding = new System.Windows.Forms.Padding(3);
            this.tab_servers.Size = new System.Drawing.Size(630, 325);
            this.tab_servers.TabIndex = 4;
            this.tab_servers.Text = "Сервер";
            this.tab_servers.UseVisualStyleBackColor = true;
            // 
            // server_ping
            // 
            this.server_ping.Location = new System.Drawing.Point(11, 171);
            this.server_ping.Name = "server_ping";
            this.server_ping.Size = new System.Drawing.Size(75, 23);
            this.server_ping.TabIndex = 15;
            this.server_ping.Text = "Ping";
            this.server_ping.UseVisualStyleBackColor = true;
            this.server_ping.Click += new System.EventHandler(this.server_ping_Click);
            // 
            // server_pass
            // 
            this.server_pass.Location = new System.Drawing.Point(123, 140);
            this.server_pass.Name = "server_pass";
            this.server_pass.ReadOnly = true;
            this.server_pass.Size = new System.Drawing.Size(497, 20);
            this.server_pass.TabIndex = 7;
            this.server_pass.TextChanged += new System.EventHandler(this.server_name_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Ключ сервера";
            // 
            // btn_apply_server
            // 
            this.btn_apply_server.Location = new System.Drawing.Point(243, 33);
            this.btn_apply_server.Name = "btn_apply_server";
            this.btn_apply_server.Size = new System.Drawing.Size(119, 23);
            this.btn_apply_server.TabIndex = 4;
            this.btn_apply_server.Text = "Применить";
            this.btn_apply_server.UseVisualStyleBackColor = true;
            this.btn_apply_server.Click += new System.EventHandler(this.btn_apply_server_Click);
            // 
            // server_link
            // 
            this.server_link.Location = new System.Drawing.Point(123, 104);
            this.server_link.Name = "server_link";
            this.server_link.ReadOnly = true;
            this.server_link.Size = new System.Drawing.Size(497, 20);
            this.server_link.TabIndex = 6;
            this.server_link.TextChanged += new System.EventHandler(this.server_name_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ссылка управления";
            // 
            // server_name
            // 
            this.server_name.Location = new System.Drawing.Point(123, 71);
            this.server_name.Name = "server_name";
            this.server_name.ReadOnly = true;
            this.server_name.Size = new System.Drawing.Size(497, 20);
            this.server_name.TabIndex = 5;
            this.server_name.TextChanged += new System.EventHandler(this.server_name_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Имя сервера";
            // 
            // btn_delete_server
            // 
            this.btn_delete_server.Location = new System.Drawing.Point(123, 33);
            this.btn_delete_server.Name = "btn_delete_server";
            this.btn_delete_server.Size = new System.Drawing.Size(114, 23);
            this.btn_delete_server.TabIndex = 3;
            this.btn_delete_server.Text = "Удалить сервер";
            this.btn_delete_server.UseVisualStyleBackColor = true;
            this.btn_delete_server.Click += new System.EventHandler(this.btn_delete_server_Click);
            // 
            // btn_add_server
            // 
            this.btn_add_server.Location = new System.Drawing.Point(8, 33);
            this.btn_add_server.Name = "btn_add_server";
            this.btn_add_server.Size = new System.Drawing.Size(109, 23);
            this.btn_add_server.TabIndex = 2;
            this.btn_add_server.Text = "Добавить сервер";
            this.btn_add_server.UseVisualStyleBackColor = true;
            this.btn_add_server.Click += new System.EventHandler(this.btn_add_server_Click);
            // 
            // cb_servers
            // 
            this.cb_servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_servers.FormattingEnabled = true;
            this.cb_servers.Location = new System.Drawing.Point(3, 6);
            this.cb_servers.Name = "cb_servers";
            this.cb_servers.Size = new System.Drawing.Size(617, 21);
            this.cb_servers.TabIndex = 1;
            this.cb_servers.SelectedIndexChanged += new System.EventHandler(this.cb_servers_SelectedIndexChanged);
            this.cb_servers.DataSourceChanged += new System.EventHandler(this.cb_servers_DataSourceChanged);
            // 
            // tab_domains
            // 
            this.tab_domains.Controls.Add(this.subs);
            this.tab_domains.Controls.Add(this.label10);
            this.tab_domains.Controls.Add(this.domain_subs);
            this.tab_domains.Controls.Add(this.label9);
            this.tab_domains.Controls.Add(this.domain_dns);
            this.tab_domains.Controls.Add(this.label8);
            this.tab_domains.Controls.Add(this.label7);
            this.tab_domains.Controls.Add(this.domain_name);
            this.tab_domains.Controls.Add(this.btn_apply_domain);
            this.tab_domains.Controls.Add(this.btn_delete_domain);
            this.tab_domains.Controls.Add(this.btn_add_domain);
            this.tab_domains.Controls.Add(this.cb_domains);
            this.tab_domains.Location = new System.Drawing.Point(4, 25);
            this.tab_domains.Name = "tab_domains";
            this.tab_domains.Padding = new System.Windows.Forms.Padding(3);
            this.tab_domains.Size = new System.Drawing.Size(630, 325);
            this.tab_domains.TabIndex = 2;
            this.tab_domains.Text = "Списки";
            this.tab_domains.UseVisualStyleBackColor = true;
            // 
            // subs
            // 
            this.subs.Location = new System.Drawing.Point(454, 88);
            this.subs.MaxLength = 100;
            this.subs.Name = "subs";
            this.subs.ReadOnly = true;
            this.subs.Size = new System.Drawing.Size(166, 20);
            this.subs.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(377, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Поддоменов";
            // 
            // domain_subs
            // 
            this.domain_subs.Location = new System.Drawing.Point(82, 114);
            this.domain_subs.Multiline = true;
            this.domain_subs.Name = "domain_subs";
            this.domain_subs.ReadOnly = true;
            this.domain_subs.Size = new System.Drawing.Size(538, 205);
            this.domain_subs.TabIndex = 7;
            this.domain_subs.TextChanged += new System.EventHandler(this.domain_name_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Поддомены";
            // 
            // domain_dns
            // 
            this.domain_dns.Location = new System.Drawing.Point(82, 88);
            this.domain_dns.MaxLength = 256;
            this.domain_dns.Name = "domain_dns";
            this.domain_dns.ReadOnly = true;
            this.domain_dns.Size = new System.Drawing.Size(280, 20);
            this.domain_dns.TabIndex = 6;
            this.domain_dns.TextChanged += new System.EventHandler(this.domain_name_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Домен";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Имя списка";
            // 
            // domain_name
            // 
            this.domain_name.Location = new System.Drawing.Point(82, 62);
            this.domain_name.MaxLength = 256;
            this.domain_name.Name = "domain_name";
            this.domain_name.ReadOnly = true;
            this.domain_name.Size = new System.Drawing.Size(538, 20);
            this.domain_name.TabIndex = 5;
            this.domain_name.TextChanged += new System.EventHandler(this.domain_name_TextChanged);
            // 
            // btn_apply_domain
            // 
            this.btn_apply_domain.Location = new System.Drawing.Point(243, 33);
            this.btn_apply_domain.Name = "btn_apply_domain";
            this.btn_apply_domain.Size = new System.Drawing.Size(119, 23);
            this.btn_apply_domain.TabIndex = 4;
            this.btn_apply_domain.Text = "Применить";
            this.btn_apply_domain.UseVisualStyleBackColor = true;
            this.btn_apply_domain.Click += new System.EventHandler(this.btn_apply_domain_Click);
            // 
            // btn_delete_domain
            // 
            this.btn_delete_domain.Location = new System.Drawing.Point(123, 33);
            this.btn_delete_domain.Name = "btn_delete_domain";
            this.btn_delete_domain.Size = new System.Drawing.Size(114, 23);
            this.btn_delete_domain.TabIndex = 3;
            this.btn_delete_domain.Text = "Удалить список";
            this.btn_delete_domain.UseVisualStyleBackColor = true;
            this.btn_delete_domain.Click += new System.EventHandler(this.btn_delete_domain_Click);
            // 
            // btn_add_domain
            // 
            this.btn_add_domain.Location = new System.Drawing.Point(8, 33);
            this.btn_add_domain.Name = "btn_add_domain";
            this.btn_add_domain.Size = new System.Drawing.Size(109, 23);
            this.btn_add_domain.TabIndex = 2;
            this.btn_add_domain.Text = "Добавить список";
            this.btn_add_domain.UseVisualStyleBackColor = true;
            this.btn_add_domain.Click += new System.EventHandler(this.btn_add_domain_Click);
            // 
            // cb_domains
            // 
            this.cb_domains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_domains.FormattingEnabled = true;
            this.cb_domains.Location = new System.Drawing.Point(3, 6);
            this.cb_domains.Name = "cb_domains";
            this.cb_domains.Size = new System.Drawing.Size(617, 21);
            this.cb_domains.TabIndex = 1;
            this.cb_domains.SelectedIndexChanged += new System.EventHandler(this.cb_domains_SelectedIndexChanged);
            this.cb_domains.DataSourceChanged += new System.EventHandler(this.cb_domains_DataSourceChanged);
            // 
            // tab_keys
            // 
            this.tab_keys.Controls.Add(this.groupBox_keys);
            this.tab_keys.Location = new System.Drawing.Point(4, 25);
            this.tab_keys.Name = "tab_keys";
            this.tab_keys.Padding = new System.Windows.Forms.Padding(3);
            this.tab_keys.Size = new System.Drawing.Size(630, 325);
            this.tab_keys.TabIndex = 1;
            this.tab_keys.Text = "Ключи";
            this.tab_keys.UseVisualStyleBackColor = true;
            // 
            // groupBox_keys
            // 
            this.groupBox_keys.Controls.Add(this.groupBox_export);
            this.groupBox_keys.Controls.Add(this.keyLength);
            this.groupBox_keys.Controls.Add(this.keyPin);
            this.groupBox_keys.Controls.Add(this.label3);
            this.groupBox_keys.Controls.Add(this.btn_apply_key);
            this.groupBox_keys.Controls.Add(this.label2);
            this.groupBox_keys.Controls.Add(this.label1);
            this.groupBox_keys.Controls.Add(this.key_name);
            this.groupBox_keys.Controls.Add(this.btn_delete_key);
            this.groupBox_keys.Controls.Add(this.btn_export_key);
            this.groupBox_keys.Controls.Add(this.btn_add_key);
            this.groupBox_keys.Controls.Add(this.cb_keys);
            this.groupBox_keys.Location = new System.Drawing.Point(0, 0);
            this.groupBox_keys.Name = "groupBox_keys";
            this.groupBox_keys.Size = new System.Drawing.Size(630, 328);
            this.groupBox_keys.TabIndex = 12;
            this.groupBox_keys.TabStop = false;
            // 
            // groupBox_export
            // 
            this.groupBox_export.Controls.Add(this.button1);
            this.groupBox_export.Controls.Add(this.btn_export_keycer);
            this.groupBox_export.Controls.Add(this.btn_export_openssl);
            this.groupBox_export.Location = new System.Drawing.Point(5, 155);
            this.groupBox_export.Name = "groupBox_export";
            this.groupBox_export.Size = new System.Drawing.Size(622, 170);
            this.groupBox_export.TabIndex = 8;
            this.groupBox_export.TabStop = false;
            this.groupBox_export.Text = "Экспортирование";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_export_keycer
            // 
            this.btn_export_keycer.Location = new System.Drawing.Point(90, 19);
            this.btn_export_keycer.Name = "btn_export_keycer";
            this.btn_export_keycer.Size = new System.Drawing.Size(75, 23);
            this.btn_export_keycer.TabIndex = 1;
            this.btn_export_keycer.Text = "cer";
            this.btn_export_keycer.UseVisualStyleBackColor = true;
            this.btn_export_keycer.Click += new System.EventHandler(this.btn_export_keycer_Click);
            // 
            // btn_export_openssl
            // 
            this.btn_export_openssl.Location = new System.Drawing.Point(9, 19);
            this.btn_export_openssl.Name = "btn_export_openssl";
            this.btn_export_openssl.Size = new System.Drawing.Size(75, 23);
            this.btn_export_openssl.TabIndex = 0;
            this.btn_export_openssl.Text = "openssl";
            this.btn_export_openssl.UseVisualStyleBackColor = true;
            this.btn_export_openssl.Click += new System.EventHandler(this.btn_export_openssl_Click);
            // 
            // keyLength
            // 
            this.keyLength.Location = new System.Drawing.Point(116, 104);
            this.keyLength.Name = "keyLength";
            this.keyLength.ReadOnly = true;
            this.keyLength.Size = new System.Drawing.Size(100, 20);
            this.keyLength.TabIndex = 6;
            // 
            // keyPin
            // 
            this.keyPin.Location = new System.Drawing.Point(116, 129);
            this.keyPin.Name = "keyPin";
            this.keyPin.ReadOnly = true;
            this.keyPin.Size = new System.Drawing.Size(360, 20);
            this.keyPin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "sha256-pinning";
            // 
            // btn_apply_key
            // 
            this.btn_apply_key.Location = new System.Drawing.Point(357, 43);
            this.btn_apply_key.Name = "btn_apply_key";
            this.btn_apply_key.Size = new System.Drawing.Size(119, 23);
            this.btn_apply_key.TabIndex = 4;
            this.btn_apply_key.Text = "Применить";
            this.btn_apply_key.UseVisualStyleBackColor = true;
            this.btn_apply_key.Click += new System.EventHandler(this.btn_apply_key_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Длина ключа(бит)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Имя ключа";
            // 
            // key_name
            // 
            this.key_name.Location = new System.Drawing.Point(116, 72);
            this.key_name.Name = "key_name";
            this.key_name.ReadOnly = true;
            this.key_name.Size = new System.Drawing.Size(506, 20);
            this.key_name.TabIndex = 5;
            this.key_name.TextChanged += new System.EventHandler(this.key_name_TextChanged);
            // 
            // btn_delete_key
            // 
            this.btn_delete_key.Location = new System.Drawing.Point(237, 43);
            this.btn_delete_key.Name = "btn_delete_key";
            this.btn_delete_key.Size = new System.Drawing.Size(114, 23);
            this.btn_delete_key.TabIndex = 3;
            this.btn_delete_key.Text = "Удалить ключ";
            this.btn_delete_key.UseVisualStyleBackColor = true;
            this.btn_delete_key.Click += new System.EventHandler(this.btn_delete_key_Click);
            // 
            // btn_export_key
            // 
            this.btn_export_key.Location = new System.Drawing.Point(125, 43);
            this.btn_export_key.Name = "btn_export_key";
            this.btn_export_key.Size = new System.Drawing.Size(106, 23);
            this.btn_export_key.TabIndex = 2;
            this.btn_export_key.Text = "Экспорт ключа";
            this.btn_export_key.UseVisualStyleBackColor = true;
            this.btn_export_key.Click += new System.EventHandler(this.btn_export_key_Click);
            // 
            // btn_add_key
            // 
            this.btn_add_key.Location = new System.Drawing.Point(10, 43);
            this.btn_add_key.Name = "btn_add_key";
            this.btn_add_key.Size = new System.Drawing.Size(109, 23);
            this.btn_add_key.TabIndex = 1;
            this.btn_add_key.Text = "Добавить ключ";
            this.btn_add_key.UseVisualStyleBackColor = true;
            this.btn_add_key.Click += new System.EventHandler(this.btn_add_key_Click);
            // 
            // cb_keys
            // 
            this.cb_keys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_keys.FormattingEnabled = true;
            this.cb_keys.Location = new System.Drawing.Point(5, 16);
            this.cb_keys.Name = "cb_keys";
            this.cb_keys.Size = new System.Drawing.Size(617, 21);
            this.cb_keys.TabIndex = 0;
            this.cb_keys.SelectedIndexChanged += new System.EventHandler(this.cb_keys_SelectedIndexChanged);
            this.cb_keys.DataSourceChanged += new System.EventHandler(this.cb_keys_DataSourceChanged);
            // 
            // tab_begin
            // 
            this.tab_begin.Controls.Add(this.btn_close);
            this.tab_begin.Controls.Add(this.btn_reset);
            this.tab_begin.Controls.Add(this.btn_saveAs);
            this.tab_begin.Controls.Add(this.btn_save);
            this.tab_begin.Controls.Add(this.btn_new);
            this.tab_begin.Controls.Add(this.btn_open);
            this.tab_begin.Location = new System.Drawing.Point(4, 25);
            this.tab_begin.Name = "tab_begin";
            this.tab_begin.Padding = new System.Windows.Forms.Padding(3);
            this.tab_begin.Size = new System.Drawing.Size(630, 325);
            this.tab_begin.TabIndex = 0;
            this.tab_begin.Text = "Начало";
            this.tab_begin.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(534, 221);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(86, 38);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Отпустить\r\nфайл";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(534, 283);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(86, 36);
            this.btn_reset.TabIndex = 4;
            this.btn_reset.Text = "Сбросить,\r\nне сохраняя";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_saveAs
            // 
            this.btn_saveAs.Location = new System.Drawing.Point(197, 114);
            this.btn_saveAs.Name = "btn_saveAs";
            this.btn_saveAs.Size = new System.Drawing.Size(227, 35);
            this.btn_saveAs.TabIndex = 3;
            this.btn_saveAs.Text = "Сохранить как...";
            this.btn_saveAs.UseVisualStyleBackColor = true;
            this.btn_saveAs.Click += new System.EventHandler(this.btn_saveAs_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(197, 224);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(227, 35);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Сохранить";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(197, 72);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(227, 36);
            this.btn_new.TabIndex = 0;
            this.btn_new.Text = "Новый";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(197, 181);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(227, 37);
            this.btn_open.TabIndex = 1;
            this.btn_open.Text = "Открыть";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tab_begin);
            this.tabControl1.Controls.Add(this.tab_keys);
            this.tabControl1.Controls.Add(this.tab_domains);
            this.tabControl1.Controls.Add(this.tab_servers);
            this.tabControl1.Controls.Add(this.tab_certs);
            this.tabControl1.Controls.Add(this.tab_le);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 354);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // tab_le
            // 
            this.tab_le.Controls.Add(this.le_clear);
            this.tab_le.Controls.Add(this.le_cancel);
            this.tab_le.Controls.Add(this.le_gr);
            this.tab_le.Controls.Add(this.le_log);
            this.tab_le.Location = new System.Drawing.Point(4, 25);
            this.tab_le.Name = "tab_le";
            this.tab_le.Size = new System.Drawing.Size(630, 325);
            this.tab_le.TabIndex = 6;
            this.tab_le.Text = "LE";
            this.tab_le.UseVisualStyleBackColor = true;
            // 
            // le_clear
            // 
            this.le_clear.Location = new System.Drawing.Point(545, 37);
            this.le_clear.Name = "le_clear";
            this.le_clear.Size = new System.Drawing.Size(75, 23);
            this.le_clear.TabIndex = 17;
            this.le_clear.Text = "Clear LOG";
            this.le_clear.UseVisualStyleBackColor = true;
            this.le_clear.Click += new System.EventHandler(this.le_clear_Click);
            // 
            // le_cancel
            // 
            this.le_cancel.Location = new System.Drawing.Point(545, 8);
            this.le_cancel.Name = "le_cancel";
            this.le_cancel.Size = new System.Drawing.Size(75, 23);
            this.le_cancel.TabIndex = 16;
            this.le_cancel.Text = "Отмена";
            this.le_cancel.UseVisualStyleBackColor = true;
            this.le_cancel.Click += new System.EventHandler(this.le_cancel_Click);
            // 
            // le_gr
            // 
            this.le_gr.Controls.Add(this.le_privKey);
            this.le_gr.Controls.Add(this.le_server);
            this.le_gr.Controls.Add(this.label14);
            this.le_gr.Controls.Add(this.label19);
            this.le_gr.Controls.Add(this.le_userKey);
            this.le_gr.Controls.Add(this.le_getCERT);
            this.le_gr.Controls.Add(this.label16);
            this.le_gr.Controls.Add(this.le_makeCSR);
            this.le_gr.Controls.Add(this.label17);
            this.le_gr.Controls.Add(this.le_dTest);
            this.le_gr.Controls.Add(this.le_domain);
            this.le_gr.Controls.Add(this.label18);
            this.le_gr.Controls.Add(this.le_reg);
            this.le_gr.Controls.Add(this.le_defDomain);
            this.le_gr.Location = new System.Drawing.Point(0, 3);
            this.le_gr.Name = "le_gr";
            this.le_gr.Size = new System.Drawing.Size(423, 136);
            this.le_gr.TabIndex = 16;
            this.le_gr.TabStop = false;
            // 
            // le_privKey
            // 
            this.le_privKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.le_privKey.FormattingEnabled = true;
            this.le_privKey.Location = new System.Drawing.Point(53, 85);
            this.le_privKey.Name = "le_privKey";
            this.le_privKey.Size = new System.Drawing.Size(239, 21);
            this.le_privKey.TabIndex = 3;
            this.le_privKey.SelectedIndexChanged += new System.EventHandler(this.le_privKey_SelectedIndexChanged);
            // 
            // le_server
            // 
            this.le_server.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.le_server.FormattingEnabled = true;
            this.le_server.Location = new System.Drawing.Point(53, 112);
            this.le_server.Name = "le_server";
            this.le_server.Size = new System.Drawing.Size(239, 21);
            this.le_server.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "userKey";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(2, 115);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Server";
            // 
            // le_userKey
            // 
            this.le_userKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.le_userKey.FormattingEnabled = true;
            this.le_userKey.Location = new System.Drawing.Point(53, 5);
            this.le_userKey.Name = "le_userKey";
            this.le_userKey.Size = new System.Drawing.Size(239, 21);
            this.le_userKey.TabIndex = 1;
            this.le_userKey.SelectedIndexChanged += new System.EventHandler(this.le_userKey_SelectedIndexChanged);
            // 
            // le_getCERT
            // 
            this.le_getCERT.Location = new System.Drawing.Point(298, 83);
            this.le_getCERT.Name = "le_getCERT";
            this.le_getCERT.Size = new System.Drawing.Size(75, 23);
            this.le_getCERT.TabIndex = 13;
            this.le_getCERT.Text = "CERT";
            this.le_getCERT.UseVisualStyleBackColor = true;
            this.le_getCERT.Click += new System.EventHandler(this.le_getCERT_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Priv. Key";
            // 
            // le_makeCSR
            // 
            this.le_makeCSR.Location = new System.Drawing.Point(298, 56);
            this.le_makeCSR.Name = "le_makeCSR";
            this.le_makeCSR.Size = new System.Drawing.Size(75, 23);
            this.le_makeCSR.TabIndex = 12;
            this.le_makeCSR.Text = "CSR";
            this.le_makeCSR.UseVisualStyleBackColor = true;
            this.le_makeCSR.Click += new System.EventHandler(this.le_makeCSR_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(2, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Domains";
            // 
            // le_dTest
            // 
            this.le_dTest.Location = new System.Drawing.Point(298, 29);
            this.le_dTest.Name = "le_dTest";
            this.le_dTest.Size = new System.Drawing.Size(75, 23);
            this.le_dTest.TabIndex = 11;
            this.le_dTest.Text = "le_dTest";
            this.le_dTest.UseVisualStyleBackColor = true;
            this.le_dTest.Click += new System.EventHandler(this.le_dTest_Click);
            // 
            // le_domain
            // 
            this.le_domain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.le_domain.FormattingEnabled = true;
            this.le_domain.Location = new System.Drawing.Point(53, 31);
            this.le_domain.Name = "le_domain";
            this.le_domain.Size = new System.Drawing.Size(239, 21);
            this.le_domain.TabIndex = 5;
            this.le_domain.SelectedIndexChanged += new System.EventHandler(this.le_domain_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(2, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "default";
            // 
            // le_reg
            // 
            this.le_reg.Location = new System.Drawing.Point(298, 5);
            this.le_reg.Name = "le_reg";
            this.le_reg.Size = new System.Drawing.Size(49, 23);
            this.le_reg.TabIndex = 9;
            this.le_reg.Text = "le_reg";
            this.le_reg.UseVisualStyleBackColor = true;
            this.le_reg.Click += new System.EventHandler(this.le_reg_Click);
            // 
            // le_defDomain
            // 
            this.le_defDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.le_defDomain.FormattingEnabled = true;
            this.le_defDomain.Location = new System.Drawing.Point(53, 58);
            this.le_defDomain.Name = "le_defDomain";
            this.le_defDomain.Size = new System.Drawing.Size(239, 21);
            this.le_defDomain.TabIndex = 7;
            this.le_defDomain.SelectedIndexChanged += new System.EventHandler(this.le_defDomain_SelectedIndexChanged);
            // 
            // le_log
            // 
            this.le_log.Location = new System.Drawing.Point(0, 142);
            this.le_log.Multiline = true;
            this.le_log.Name = "le_log";
            this.le_log.ReadOnly = true;
            this.le_log.Size = new System.Drawing.Size(630, 183);
            this.le_log.TabIndex = 8;
            // 
            // le_backgr
            // 
            this.le_backgr.WorkerReportsProgress = true;
            this.le_backgr.WorkerSupportsCancellation = true;
            this.le_backgr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.le_backgr_DoWork);
            this.le_backgr.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.le_backgr_ProgressChanged);
            this.le_backgr.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.le_backgr_RunWorkerCompleted);
            // 
            // tExpired
            // 
            this.tExpired.Location = new System.Drawing.Point(445, 129);
            this.tExpired.Name = "tExpired";
            this.tExpired.ReadOnly = true;
            this.tExpired.Size = new System.Drawing.Size(177, 20);
            this.tExpired.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 353);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "shamka LE updater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tab_certs.ResumeLayout(false);
            this.groupBox_certs.ResumeLayout(false);
            this.groupBox_certs.PerformLayout();
            this.groupBox_export_certs.ResumeLayout(false);
            this.tab_servers.ResumeLayout(false);
            this.tab_servers.PerformLayout();
            this.tab_domains.ResumeLayout(false);
            this.tab_domains.PerformLayout();
            this.tab_keys.ResumeLayout(false);
            this.groupBox_keys.ResumeLayout(false);
            this.groupBox_keys.PerformLayout();
            this.groupBox_export.ResumeLayout(false);
            this.tab_begin.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab_le.ResumeLayout(false);
            this.tab_le.PerformLayout();
            this.le_gr.ResumeLayout(false);
            this.le_gr.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tab_certs;
        private System.Windows.Forms.TabPage tab_servers;
        private System.Windows.Forms.Button btn_delete_server;
        private System.Windows.Forms.Button btn_add_server;
        private System.Windows.Forms.ComboBox cb_servers;
        private System.Windows.Forms.TabPage tab_domains;
        private System.Windows.Forms.Button btn_delete_domain;
        private System.Windows.Forms.Button btn_add_domain;
        private System.Windows.Forms.ComboBox cb_domains;
        private System.Windows.Forms.TabPage tab_keys;
        private System.Windows.Forms.Button btn_delete_key;
        private System.Windows.Forms.Button btn_export_key;
        private System.Windows.Forms.Button btn_add_key;
        private System.Windows.Forms.ComboBox cb_keys;
        private System.Windows.Forms.TabPage tab_begin;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox key_name;
        private System.Windows.Forms.Button btn_apply_key;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox keyLength;
        private System.Windows.Forms.TextBox keyPin;
        private System.Windows.Forms.GroupBox groupBox_keys;
        private System.Windows.Forms.GroupBox groupBox_export;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_saveAs;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_apply_domain;
        private System.Windows.Forms.TextBox server_pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_apply_server;
        private System.Windows.Forms.TextBox server_link;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox server_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox domain_dns;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox domain_name;
        private System.Windows.Forms.TextBox domain_subs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox subs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button server_ping;
        private System.Windows.Forms.GroupBox groupBox_certs;
        private System.Windows.Forms.GroupBox groupBox_export_certs;
        private System.Windows.Forms.TextBox certs_length;
        private System.Windows.Forms.TextBox certs_pin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_apply_certs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox certs_name;
        private System.Windows.Forms.Button btn_delete_certs;
        private System.Windows.Forms.Button btn_export_certs;
        private System.Windows.Forms.Button btn_add_certs;
        private System.Windows.Forms.ComboBox cb_certs;
        private System.Windows.Forms.TextBox certs_finger;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_export_openssl;
        private System.Windows.Forms.Button btn_certs_cer;
        private System.Windows.Forms.TextBox certs_cns;
        private System.Windows.Forms.Button btn_certs_cer_chain;
        private System.Windows.Forms.TabPage tab_le;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox le_log;
        private System.Windows.Forms.ComboBox le_defDomain;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox le_domain;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox le_privKey;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox le_userKey;
        private System.Windows.Forms.Button le_reg;
        private System.Windows.Forms.Button le_makeCSR;
        private System.Windows.Forms.Button le_dTest;
        private System.Windows.Forms.Button le_getCERT;
        private System.Windows.Forms.Button btn_export_keycer;
        private System.Windows.Forms.ComboBox le_server;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox le_gr;
        private System.ComponentModel.BackgroundWorker le_backgr;
        private System.Windows.Forms.Button le_cancel;
        private System.Windows.Forms.Button le_clear;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tExpired;
    }
}

