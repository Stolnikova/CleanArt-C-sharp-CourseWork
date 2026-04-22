-- Seed admin users
-- Password for both: admin123
INSERT INTO "Users" ("Login", "PasswordHash", "Role")
VALUES
    ('admin1', '$2a$11$oswC1Gz2SuIvp5M.UR0bJeIeSu5yjnM1MqqWU48Vb9yrya0/GUz66', 'Admin'),
    ('admin2', '$2a$11$Q3U72WoR1R2BPyHrmJrAiuH1MNm5TOiZXGNHLov7l.bTwxLQtOhqq', 'Admin');