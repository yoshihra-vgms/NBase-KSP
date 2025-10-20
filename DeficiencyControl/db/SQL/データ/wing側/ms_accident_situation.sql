--元データ全削除
DELETE FROM ms_accident_situation;

--シーケンスリセット
SELECT SETVAL ('ms_accident_situation_accident_situation_id_seq', '1', false);

--Insert
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('荷役中');
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('航海中');
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('停泊中');
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('着離桟中');
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('作業中');
INSERT INTO ms_accident_situation(accident_situation_name) VALUES ('その他');