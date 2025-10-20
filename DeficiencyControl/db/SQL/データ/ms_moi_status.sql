--元データ全削除
DELETE FROM ms_moi_status;

--シーケンスリセット
SELECT SETVAL ('ms_moi_status_moi_status_id_seq', '1', false);

--Insert
INSERT INTO ms_moi_status(moi_status_name) VALUES ('Pending');
INSERT INTO ms_moi_status(moi_status_name) VALUES ('Complete');
