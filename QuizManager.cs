using System;
using System.Collections.Generic;
using System.Linq;              

namespace demo
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
    }

    public class QuizManager
    {
        private List<Question> questions;
        private int currentIndex;
        private ActivityLogger logger;
        public int Score { get; private set; }
        public int TotalQuestions => questions.Count;
        public bool IsFinished => currentIndex >= questions.Count;

        public QuizManager(ActivityLogger logger)
        {
            this.logger = logger;
            InitializeQuestions();
            ResetQuiz();
        }

        private void InitializeQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "What is the best practice for creating a password?",
                    Options = new List<string> { "Use your birthday", "Use a long passphrase with special characters", "Use 'password123'", "Use the same password for all accounts" },
                    CorrectIndex = 1,
                    Explanation = "A long passphrase with special characters is much harder to crack."
                },
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Phishing emails often ask for sensitive info. Report them and never reply."
                },
                new Question
                {
                    Text = "True or False: Using public Wi-Fi without a VPN is safe for online banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Public Wi-Fi is insecure. Always use a VPN for sensitive activities."
                },
                new Question
                {
                    Text = "What is a firewall?",
                    Options = new List<string> { "A physical wall", "A software that filters network traffic", "A type of virus", "A password manager" },
                    CorrectIndex = 1,
                    Explanation = "A firewall monitors and controls incoming and outgoing network traffic based on security rules."
                },
                new Question
                {
                    Text = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "Using two passwords", "Using a password and a second factor like a code", "Using biometrics only", "Using a single factor" },
                    CorrectIndex = 1,
                    Explanation = "2FA adds an extra layer of security by requiring a second factor, such as a code from your phone."
                },
                new Question
                {
                    Text = "Which of the following is a sign of a phishing attempt?",
                    Options = new List<string> { "A generic greeting", "Urgent language asking for immediate action", "Suspicious sender address", "All of the above" },
                    CorrectIndex = 3,
                    Explanation = "Phishing emails often have these red flags. Always verify before clicking."
                },
                new Question
                {
                    Text = "True or False: It's safe to download attachments from unknown senders.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Attachments from unknown senders can contain malware. Never open them."
                },
                new Question
                {
                    Text = "What does 'phishing' mean?",
                    Options = new List<string> { "A sport", "A type of malware", "A scam to steal personal info", "A security protocol" },
                    CorrectIndex = 2,
                    Explanation = "Phishing is a fraudulent attempt to obtain sensitive information by disguising as a trustworthy entity."
                },
                new Question
                {
                    Text = "How often should you update your software?",
                    Options = new List<string> { "Once a year", "Only when prompted", "Regularly as updates are released", "Never" },
                    CorrectIndex = 2,
                    Explanation = "Regular updates patch security vulnerabilities, keeping you protected."
                },
                new Question
                {
                    Text = "What is a strong password?",
                    Options = new List<string> { "A short, easy-to-remember word", "A combination of uppercase, lowercase, numbers, and symbols", "Your pet's name", "The word 'password'" },
                    CorrectIndex = 1,
                    Explanation = "Strong passwords are complex and include a mix of characters."
                },
                new Question
                {
                    Text = "True or False: You should reuse passwords across multiple sites.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Reusing passwords increases risk. If one site is breached, all are compromised."
                },
                new Question
                {
                    Text = "What is social engineering?",
                    Options = new List<string> { "Building social networks", "Manipulating people into giving up confidential info", "A type of software", "A security tool" },
                    CorrectIndex = 1,
                    Explanation = "Social engineering exploits human psychology to gain access to systems or data."
                }
            };
        }

        public void StartQuiz()
        {
            currentIndex = 0;
            Score = 0;
        }

        public Question GetCurrentQuestion()
        {
            if (IsFinished) return null;
            return questions[currentIndex];
        }

        public bool SubmitAnswer(int selectedIndex)
        {
            if (IsFinished) return false;
            var q = questions[currentIndex];
            bool correct = selectedIndex == q.CorrectIndex;
            if (correct) Score++;
            // Move to next
            currentIndex++;
            logger.LogAction($"Quiz: answered question '{q.Text}' - {(correct ? "Correct" : "Incorrect")}");
            return correct;
        }

        public void ResetQuiz()
        {
            currentIndex = 0;
            Score = 0;
        }
    }
}