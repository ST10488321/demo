# Cybersecurity Awareness Chatbot (Part 3 POE)

A WPF desktop application that helps users learn about online safety through:
- A conversational chatbot (keyword‑based + sentiment detection)
- A task manager with MySQL storage
- A 12‑question cybersecurity quiz
- An activity log that tracks everything you do

Built with C#, WPF (.NET Framework 4.8) and MySQL.

---

## Features at a glance

| Feature | What it does |
|---------|--------------|
| Chat | Responds to cybersecurity questions (Phishing, VPN, Passwords, etc.) and detects emotions. |
| Tasks | Add, complete, delete tasks – each can have a reminder date. All saved in MySQL. |
| Quiz | 12 mixed‑format questions (multiple‑choice & true/false) with instant feedback and score. |
| Activity Log | Shows the last 10 actions (logins, task changes, quiz attempts). |
| NLP‑like commands | Chat understands: *“add task”*, *“remind me to …”*, *“start quiz”*, *“show activity log”*. |

---

## 🗄️ Database setup (MySQL)

1. Create a database called `cyberchat`.
2. Run this script to create the `tasks` table:

```sql
CREATE TABLE tasks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    reminder_date DATETIME NULL,
    is_completed BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
