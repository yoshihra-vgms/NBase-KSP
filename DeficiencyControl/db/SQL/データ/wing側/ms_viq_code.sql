--元データ全削除
DELETE FROM ms_viq_code;

--シーケンスリセット
SELECT SETVAL ('ms_viq_code_viq_code_id_seq', '1', false);

--Insert
--Ver.6
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('1', 1, 1, '一般情報', 'General Information', 1);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('2', 2, 1, '証書及び文書', 'Certification and documentation', 2);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('3', 3, 1, '乗組員の管理', 'Crew management', 3);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('4', 4, 1, '航海', 'Navigation', 4);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('5', 5, 1, '安全管理', 'Safety management', 5);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('6', 6, 1, '汚染防止', 'Pollution prevention', 6);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('7', 7, 1, '構造物の状態', 'Structural condition', 7);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8P', 8, 1, '貨物及びﾊﾞﾗｽﾄｼｽﾃﾑ―石油', 'Cargo and ballast systems - petroleum', 8);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8C', 8, 1, '貨物及びﾊﾞﾗｽﾄｼｽﾃﾑ―ｹﾐｶﾙ', 'Cargo and ballast systems - chemical', 9);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8G', 8, 1, '貨物及びﾊﾞﾗｽﾄｼｽﾃﾑ―LPG', 'Cargo and ballast systems - LPG', 10);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8L', 8, 1, '貨物及びﾊﾞﾗｽﾄｼｽﾃﾑ―LNG', 'Cargo and Ballast Systems - LNG', 11);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('9', 9, 1, '係船設備', 'Mooring', 12);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('10', 10, 1, '通信', 'Communications', 13);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('11', 11, 1, '機関室及び操舵機室', 'Engine and steering compartments', 14);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('12', 12, 1, '一般的外観及び状態', 'General appearance and condition', 15);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('13', 13, 1, '氷海運航', 'Ice Operations', 16);
--Ver.7
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('1', 14, 2, '一般情報', 'General Information', 1);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('2', 15, 2, '証書及び文書', 'Certification and documentation', 2);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('3', 16, 2, '乗組員の管理', 'Crew management', 3);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('4', 17, 2, '航海ならびに通信', 'Navigation and Communications', 4);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('5', 18, 2, '安全管理', 'Safety management', 5);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('6', 19, 2, '汚染防止', 'Pollution prevention', 6);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('7', 20, 2, '海事保安', 'Maritime Security', 7);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8P', 21, 2, '貨物及びﾊﾞﾗｽﾄ装置―石油', 'Cargo and ballast systems - Petroleum', 8);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8C', 21, 2, '貨物及びﾊﾞﾗｽﾄ装置―ｹﾐｶﾙ', 'Cargo and ballast systems - Chemical', 9);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8G', 21, 2, '貨物及びﾊﾞﾗｽﾄ装置―LPG', 'Cargo and ballast systems - LPG', 10);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('8L', 21, 2, '貨物及びﾊﾞﾗｽﾄ装置―LNG', 'Cargo and Ballast Systems - LNG', 11);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('9', 22, 2, '係船設備', 'Mooring', 12);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('10', 23, 2, '機関室及び操舵機室', 'Engine and steering compartments', 13);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('11', 24, 2, '一般的外観及び状態', 'General appearance and condition', 14);
INSERT INTO ms_viq_code(viq_code, viq_code_name_id, viq_version_id, description, description_eng, order_no) VALUES ('12', 25, 2, '氷海運航', 'Ice Operations', 15);


