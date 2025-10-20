--元データ全削除
DELETE FROM ms_accident_status;

--シーケンスリセット
SELECT SETVAL ('ms_accident_status_accident_status_id_seq', '1', false);

--Insert
INSERT INTO ms_accident_status(accident_status_name) VALUES ('Pending');
INSERT INTO ms_accident_status(accident_status_name) VALUES ('Complete');
