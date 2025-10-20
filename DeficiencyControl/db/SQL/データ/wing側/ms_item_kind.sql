--元データ全削除 
DELETE FROM ms_item_kind;

--シーケンスリセット
SELECT SETVAL ('ms_item_kind_item_kind_id_seq', '1', false);

--Insert
INSERT INTO ms_item_kind(item_kind_name, delete_flag) VALUES ('PSC Inspection', false);
