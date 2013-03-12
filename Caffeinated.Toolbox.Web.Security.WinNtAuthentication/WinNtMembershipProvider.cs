namespace Caffeinated.Toolbox.Web.Security.MembershipProviders
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    public class WinNtMembershipProvider : MembershipProvider
    {
        private int logonType = 2;
        private string name = "WinNtMembershipProvider";
        private string userDomain = string.Empty;

        public WinNtMembershipProvider()
        {
            this.ApplicationName = "DefaultApp";
        }

        public override sealed string ApplicationName { get; set; }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 5; }
        }

        public override string Name
        {
            get { return this.name; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotSupportedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return "[\\w| !§$%&amp;/()=\\-?\\*]*"; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
                                                  string passwordQuestion, string passwordAnswer, bool isApproved,
                                                  object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override void Initialize(string providerName, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (string.IsNullOrWhiteSpace(providerName))
            {
                providerName = "WinNtMembershipProvider";
            }

            if (string.IsNullOrWhiteSpace(config["description"]))
            {
                config.Set("description", "WinNT Membership Provider");
            }

            base.Initialize(providerName, config);

            foreach (string index in config.Keys)
            {
                switch (index.ToLower())
                {
                    case "name":
                        this.name = config[index];
                        continue;
                    case "applicationname":
                        this.ApplicationName = config[index];
                        continue;
                    case "userdomain":
                        this.userDomain = config[index];
                        continue;
                    case "logontype":
                        this.logonType = int.Parse(config[index]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        [DllImport("ADVAPI32.dll", EntryPoint = "LogonUserW", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool LogonUser(string userName, string domain, string password, int logonType,
                                             int logonProvider, ref IntPtr token);

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var token = IntPtr.Zero;
            return LogonUser(username, this.userDomain, password, this.logonType, 0, ref token);
        }
    }
}