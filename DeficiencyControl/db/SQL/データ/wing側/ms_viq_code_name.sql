--元データ全削除
DELETE FROM ms_viq_code_name;

--シーケンスリセット
SELECT SETVAL ('ms_viq_code_name_viq_code_name_id_seq', '1', false);

--Insert
--Ver.6
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('1', '一般情報', 'General Information', 1);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('2', '証書及び文書', 'Certification and documentation', 2);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('3', '乗組員の管理', 'Crew management', 3);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('4', '航海', 'Navigation', 4);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('5', '安全管理', 'Safety management', 5);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('6', '汚染防止', 'Pollution prevention', 6);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('7', '構造物の状態', 'Structural condition', 7);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('8', '貨物及びﾊﾞﾗｽﾄｼｽﾃﾑ', 'Cargo and ballast systems ? petroleum', 8);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('9', '係船設備', 'Mooring', 9);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('10', '通信', 'Communications', 10);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('11', '機関室及び操舵機室', 'Engine and steering compartments', 11);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('12', '一般的外観及び状態', 'General appearance and condition', 12);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('13', '氷海運航', 'Ice Operations', 13);
--Ver.7
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('1', '一般情報', 'General Information', 14);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('2', '証書及び文書', 'Certification and documentation', 15);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('3', '乗組員の管理', 'Crew management', 16);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('4', '航海ならびに通信', 'Navigation and Communications', 17);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('5', '安全管理', 'Safety management', 18);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('6', '汚染防止', 'Pollution prevention', 19);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('7', '海事保安', 'Maritime Security', 20);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('8', '貨物及びﾊﾞﾗｽﾄ装置', 'Cargo and Ballast Systems', 21);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('9', '係船設備', 'Mooring', 22);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('10', '機関室及び操舵機室', 'Engine and steering compartments', 23);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('11', '一般的外観及び状態', 'General appearance and condition', 24);
INSERT INTO ms_viq_code_name(viq_code_name, description, description_eng, order_no) VALUES ('12', '氷海運航', 'Ice Operations', 25);
