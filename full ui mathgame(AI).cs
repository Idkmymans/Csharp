using System;
using System.Drawing;
using System.Windows;

namespace MathGame
{
    public partial class MathGameForm : Form
    {
        // Game variables
        private Random random = new Random();
        private int score = 0;
        private int totalQuestions = 0;
        private int currentAnswer;
        private Timer gameTimer;
        private int timeLeft;
        private string currentOperation;
        
        // UI Controls
        private Label lblProblem;
        private TextBox txtAnswer;
        private Button btnSubmit;
        private Button btnNewGame;
        private Label lblScore;
        private Label lblTimer;
        private ComboBox cmbDifficulty;
        private ComboBox cmbOperation;
        private Label lblFeedback;
        private ProgressBar progressBar;

        public MathGameForm()
        {
            InitializeComponent();
            SetupTimer();
            GenerateNewProblem();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Math Game";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            // Problem label
            lblProblem = new Label
            {
                Location = new Point(150, 50),
                Size = new Size(200, 50),
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Answer textbox
            txtAnswer = new TextBox
            {
                Location = new Point(175, 120),
                Size = new Size(150, 30),
                Font = new Font("Arial", 14),
                TextAlign = HorizontalAlignment.Center
            };
            txtAnswer.KeyPress += TxtAnswer_KeyPress;

            // Submit button
            btnSubmit = new Button
            {
                Location = new Point(200, 160),
                Size = new Size(100, 35),
                Text = "Submit",
                Font = new Font("Arial", 12),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += BtnSubmit_Click;

            // New Game button
            btnNewGame = new Button
            {
                Location = new Point(350, 20),
                Size = new Size(100, 30),
                Text = "New Game",
                Font = new Font("Arial", 10),
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            btnNewGame.Click += BtnNewGame_Click;

            // Score label
            lblScore = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(150, 30),
                Text = "Score: 0/0",
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            // Timer label
            lblTimer = new Label
            {
                Location = new Point(200, 20),
                Size = new Size(100, 30),
                Text = "Time: 30",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };

            // Difficulty combo box
            Label lblDifficultyLabel = new Label
            {
                Location = new Point(20, 220),
                Size = new Size(80, 25),
                Text = "Difficulty:",
                Font = new Font("Arial", 10)
            };

            cmbDifficulty = new ComboBox
            {
                Location = new Point(100, 220),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDifficulty.Items.AddRange(new[] { "Easy", "Medium", "Hard" });
            cmbDifficulty.SelectedIndex = 0;
            cmbDifficulty.SelectedIndexChanged += CmbDifficulty_SelectedIndexChanged;

            // Operation combo box
            Label lblOperationLabel = new Label
            {
                Location = new Point(220, 220),
                Size = new Size(80, 25),
                Text = "Operation:",
                Font = new Font("Arial", 10)
            };

            cmbOperation = new ComboBox
            {
                Location = new Point(300, 220),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbOperation.Items.AddRange(new[] { "Mixed", "Addition", "Subtraction", "Multiplication", "Division" });
            cmbOperation.SelectedIndex = 0;

            // Feedback label
            lblFeedback = new Label
            {
                Location = new Point(150, 260),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = ""
            };

            // Progress bar
            progressBar = new ProgressBar
            {
                Location = new Point(50, 310),
                Size = new Size(400, 20),
                Maximum = 30,
                Value = 30
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblProblem, txtAnswer, btnSubmit, btnNewGame,
                lblScore, lblTimer, lblDifficultyLabel, cmbDifficulty,
                lblOperationLabel, cmbOperation, lblFeedback, progressBar
            });
        }

        private void SetupTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 1000; // 1 second
            gameTimer.Tick += GameTimer_Tick;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTimer.Text = $"Time: {timeLeft}";
            progressBar.Value = Math.Max(0, timeLeft);

            if (timeLeft <= 5)
            {
                lblTimer.ForeColor = Color.Red;
            }

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                ShowFeedback("Time's up!", Color.Red);
                totalQuestions++;
                UpdateScore();
                GenerateNewProblem();
            }
        }

        private void GenerateNewProblem()
        {
            int num1, num2;
            string operation = GetOperation();
            
            switch (cmbDifficulty.SelectedItem?.ToString())
            {
                case "Easy":
                    num1 = random.Next(1, 10);
                    num2 = random.Next(1, 10);
                    timeLeft = 30;
                    break;
                case "Medium":
                    num1 = random.Next(10, 50);
                    num2 = random.Next(10, 50);
                    timeLeft = 20;
                    break;
                case "Hard":
                    num1 = random.Next(50, 100);
                    num2 = random.Next(50, 100);
                    timeLeft = 15;
                    break;
                default:
                    num1 = random.Next(1, 10);
                    num2 = random.Next(1, 10);
                    timeLeft = 30;
                    break;
            }

            progressBar.Maximum = timeLeft;
            progressBar.Value = timeLeft;
            lblTimer.ForeColor = Color.Black;

            switch (operation)
            {
                case "+":
                    currentAnswer = num1 + num2;
                    lblProblem.Text = $"{num1} + {num2} = ?";
                    break;
                case "-":
                    // Ensure positive result
                    if (num1 < num2)
                    {
                        int temp = num1;
                        num1 = num2;
                        num2 = temp;
                    }
                    currentAnswer = num1 - num2;
                    lblProblem.Text = $"{num1} - {num2} = ?";
                    break;
                case "*":
                    currentAnswer = num1 * num2;
                    lblProblem.Text = $"{num1} × {num2} = ?";
                    break;
                case "/":
                    // Ensure clean division
                    num1 = num2 * random.Next(1, 10);
                    currentAnswer = num1 / num2;
                    lblProblem.Text = $"{num1} ÷ {num2} = ?";
                    break;
            }

            currentOperation = operation;
            txtAnswer.Clear();
            txtAnswer.Focus();
            gameTimer.Start();
        }

        private string GetOperation()
        {
            string selectedOp = cmbOperation.SelectedItem?.ToString();
            
            if (selectedOp == "Mixed")
            {
                string[] operations = { "+", "-", "*", "/" };
                return operations[random.Next(operations.Length)];
            }
            
            switch (selectedOp)
            {
                case "Addition": return "+";
                case "Subtraction": return "-";
                case "Multiplication": return "*";
                case "Division": return "/";
                default: return "+";
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void TxtAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                CheckAnswer();
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void CheckAnswer()
        {
            if (int.TryParse(txtAnswer.Text, out int userAnswer))
            {
                gameTimer.Stop();
                totalQuestions++;

                if (userAnswer == currentAnswer)
                {
                    score++;
                    ShowFeedback("Correct! Well done!", Color.Green);
                }
                else
                {
                    ShowFeedback($"Wrong! The answer was {currentAnswer}", Color.Red);
                }

                UpdateScore();
                GenerateNewProblem();
            }
            else
            {
                ShowFeedback("Please enter a valid number!", Color.Orange);
                txtAnswer.SelectAll();
                txtAnswer.Focus();
            }
        }

        private void ShowFeedback(string message, Color color)
        {
            lblFeedback.Text = message;
            lblFeedback.ForeColor = color;
            
            Timer feedbackTimer = new Timer();
            feedbackTimer.Interval = 2000;
            feedbackTimer.Tick += (s, e) =>
            {
                lblFeedback.Text = "";
                feedbackTimer.Stop();
                feedbackTimer.Dispose();
            };
            feedbackTimer.Start();
        }

        private void UpdateScore()
        {
            lblScore.Text = $"Score: {score}/{totalQuestions}";
            
            if (totalQuestions > 0)
            {
                double percentage = (double)score / totalQuestions * 100;
                if (percentage >= 80)
                    lblScore.ForeColor = Color.Green;
                else if (percentage >= 60)
                    lblScore.ForeColor = Color.Orange;
                else
                    lblScore.ForeColor = Color.Red;
            }
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            score = 0;
            totalQuestions = 0;
            UpdateScore();
            lblScore.ForeColor = Color.Black;
            GenerateNewProblem();
        }

        private void CmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateNewProblem();
        }
    }

    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MathGameForm());
        }
    }
}