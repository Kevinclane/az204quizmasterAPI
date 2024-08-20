
-- Delete user generated data
DELETE FROM quizmaster.activeqas WHERE id != NULL;
DELETE FROM quizmaster.quizzes WHERE id != NULL;

-- Only use this to remove all questions/answer objects - need to reupload json files to repopulate
DELETE FROM quizmaster.options WHERE QAId != NULL;
DELETE FROM quizmaster.resourcelink WHERE QAId != NULL;
DELETE FROM quizmaster.qas;