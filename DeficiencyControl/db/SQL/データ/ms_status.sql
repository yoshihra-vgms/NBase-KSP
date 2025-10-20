--元データ全削除 
DELETE FROM ms_status;

--シーケンスリセット
SELECT SETVAL ('ms_status_status_id_seq', '1', false);

--Insert
INSERT INTO ms_status(status_name, delete_flag) VALUES ('Pending', false);
INSERT INTO ms_status(status_name, delete_flag) VALUES ('Complete', false);