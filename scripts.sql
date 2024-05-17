
-- Reset DB
DELETE FROM quizmaster.options WHERE QAId != NULL;
DELETE FROM quizmaster.resourcelink WHERE QAId != NULL;
DELETE FROM quizmaster.qas;