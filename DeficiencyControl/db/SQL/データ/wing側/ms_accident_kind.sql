--元データ全削除
DELETE FROM ms_accident_kind;

--シーケンスリセット
SELECT SETVAL ('ms_accident_kind_accident_kind_id_seq', '1', false);

--Insert
