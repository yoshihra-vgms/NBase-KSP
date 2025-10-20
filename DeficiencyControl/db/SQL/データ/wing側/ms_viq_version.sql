--元データ全削除
DELETE FROM ms_viq_version;

--シーケンスリセット
SELECT SETVAL ('ms_viq_version_viq_version_id_seq', '1', false);

--Insert
INSERT INTO ms_viq_version(viq_version, start_date, end_date) VALUES ('Version6', '0001/1/1', '2018/9/17');
INSERT INTO ms_viq_version(viq_version, start_date, end_date) VALUES ('Version7', '2018/9/18', '9999/3/31');




