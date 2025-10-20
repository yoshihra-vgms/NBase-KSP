--元データ全削除
DELETE FROM ms_accident_importance;

--シーケンスリセット
SELECT SETVAL ('ms_accident_importance_accident_importance_id_seq', '1', false);

--Insert
INSERT INTO ms_accident_importance(accident_importance_name) VALUES ('High');
INSERT INTO ms_accident_importance(accident_importance_name) VALUES ('Medium');
INSERT INTO ms_accident_importance(accident_importance_name) VALUES ('Low');