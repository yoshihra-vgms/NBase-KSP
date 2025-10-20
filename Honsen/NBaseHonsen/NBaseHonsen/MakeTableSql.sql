# 
CREATE TABLE MS_USER
(
  MS_USER_ID varchar(40) NOT NULL PRIMARY KEY,
  USER_KBN int, 
  LOGIN_ID varchar(30),
  PASSWORD varchar(30),
  MAIL_ADDRESS varchar(30),
  ADMIN_FLAG int,
  DELETE_FLAG int,
  SEND_FLAG int,
  VESSEL_ID int,
  DATA_NO int,
  USER_KEY varchar(40),
  RENEW_DATE timestamp,
  RENEW_USER_ID varchar(40),
  TS varchar(20),
  SEI varchar(20),
  MEI varchar(20),
  SEI_KANA varchar(20),
  MEI_KANA varchar(20),
  SEX int,
  DOC_FLAG_CEO int,
  DOC_FLAG_ADMIN int,
  DOC_FLAG_MSI_FERRY int,
  DOC_FLAG_CREW_FERRY int,
  DOC_FLAG_TSI_FERRY int,
  DOC_FLAG_MSI_CARGO int,
  DOC_FLAG_CREW_CARGO int,
  DOC_FLAG_TSI_CARGO int,
  DOC_FLAG_OFFICER int,
  DOC_FLAG_GL int,
  DOC_FLAG_TL int
  );
  
CREATE TABLE MS_CUSTOMER
(
    MS_CUSTOMER_ID                 varchar(40) NOT NULL PRIMARY KEY,
    CUSTOMER_NAME                  varchar(50),
    TEL                            varchar(25),
    FAX                            varchar(25),
    ZIP_CODE                       varchar(10),
    ADDRESS1                       varchar(50),
    ADDRESS2                       varchar(50),
    BUILDING_NAME                  varchar(50),
    BANK_NAME                      varchar(20),
    BRANCH_NAME                    varchar(20),
    ACCOUNT_NO                     varchar(20),
    ACCOUNT_ID                     varchar(50),
    DELETE_FLAG                    int DEFAULT 0 NOT NULL,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    SHUBETSU                       varchar(5) NOT NULL,
    LOGIN_ID                       varchar(30),
    PASSWORD                       varchar(30)
    );

CREATE TABLE MS_LO
(
    MS_LO_ID                       varchar(40) NOT NULL PRIMARY KEY,
    LO_NAME                        varchar(50),
    KAMOKU_NO                      varchar(20),
    MS_TANI_ID                     varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));

CREATE TABLE MS_VESSEL
(
  MS_VESSEL_ID int NOT NULL PRIMARY KEY,
  VESSEL_NO varchar(10) UNIQUE,
  VESSEL_NAME varchar(30),
  DWT int,
  CAPACITY int,
  TEL varchar(25),
  MS_VESSEL_TYPE_ID varchar(40),
  DELETE_FLAG int,
  SEND_FLAG int,
  VESSEL_ID int,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp,
  RENEW_USER_ID varchar(40),
  TS varchar(20),
  HP_TEL varchar(25),
  HACHU_ENABLED int,
  YOJITSU_ENABLED int,
  HONSEN_ENABLED int,
  OWNER int,
  KANIDOUSEI_ENABLED int,
  AKASAKA_VESSEL_NO varchar(5),
  OFFICIAL_NUMBER varchar(20),
  CARGO_WEIGHT decimal(16,4),
  SHOW_ORDER int,
  COMPLETION_DATE timestamp,
  ANNIVERSARY_DATE timestamp,
  NATIONALITY varchar(25),
  DOUSEI_REPORT1 int,
  DOUSEI_REPORT2 int,
  DOUSEI_REPORT3 int,
  MAIL_ADDRESS varchar(50),
  KAIKEI_BUMON_CODE varchar(50),
  DOCUMENT_ENABLED int,
  SENIN_ENABLED numeric(1,0),
  KENSA_ENABLED numeric(1,0),
  YOJITSU_RESULTS numeric(1,0),
  HACHU_RESULTS numeric(1,0),
  SENIN_RESULTS numeric(1,0),
  DOCUMENT_RESULTS numeric(1,0),
  KENSA_RESULTS numeric(1,0),
  KANIDOUSEI_RESULTS numeric(1,0)
);
  
CREATE TABLE MS_VESSEL_ITEM
(
    MS_VESSEL_ITEM_ID              varchar(40) NOT NULL PRIMARY KEY,
    VESSEL_ITEM_NAME               varchar(50),
    KAMOKU_NO                      varchar(20),
    MS_TANI_ID                     varchar(40),
    CATEGORY_NUMBER                int DEFAULT 0 NOT NULL,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));

CREATE TABLE MS_VESSEL_TYPE
(
  MS_VESSEL_TYPE_ID varchar(40) NOT NULL PRIMARY KEY,
  VESSEL_TYPE_NAME varchar(10),
  RENEW_DATE timestamp,
  RENEW_USER_ID varchar(40),
  TS varchar(20));

CREATE TABLE MS_ITEM_SBT
(
    MS_ITEM_SBT_ID varchar(40) NOT NULL PRIMARY KEY,
    ITEM_SBT_NAME  varchar(10),
    SEND_FLAG      int DEFAULT 0 NOT NULL,
    VESSEL_ID      int NOT NULL,
    DATA_NO        int,
    USER_KEY       varchar(40) DEFAULT 0,
    RENEW_DATE     timestamp NOT NULL,
    RENEW_USER_ID  varchar(40) NOT NULL,
    TS             varchar(20),
    DELETE_FLAG int DEFAULT 0 NOT NULL
);

CREATE TABLE MS_NYUKYO_KAMOKU
(
    MS_NYUKYO_KAMOKU_ID            varchar(40) NOT NULL PRIMARY KEY,
    NYUKYO_KAMOKU_NAME             varchar(10),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    SHOW_ORDER                     int
);
    
CREATE TABLE MS_SHR_JOUKEN
(
    MS_SHR_JOUKEN_ID               varchar(40) NOT NULL PRIMARY KEY,
    SHIHARAI_JOUKEN_NAME           varchar(20),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));

CREATE TABLE MS_TANI
(
    MS_TANI_ID                     varchar(40) NOT NULL PRIMARY KEY,
    TANI_NAME                      varchar(5),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20)
);

CREATE TABLE MS_THI_IRAI_SBT
(
    MS_THI_IRAI_SBT_ID varchar(40) NOT NULL PRIMARY KEY,
    THI_IRAI_SBT_NAME varchar(10),
    SEND_FLAG int NOT NULL,
    VESSEL_ID int NOT NULL,
    DATA_NO int, RENEW_DATE timestamp,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_USER_ID varchar(40),
    TS varchar(20));

CREATE TABLE MS_THI_IRAI_SHOUSAI
(
    MS_THI_IRAI_SHOUSAI_ID         varchar(40) NOT NULL PRIMARY KEY,
    THI_IRAI_SHOUSAI_NAME          varchar(10),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));
    
CREATE TABLE MS_THI_IRAI_STATUS
(
    MS_THI_IRAI_STATUS_ID          varchar(40) NOT NULL PRIMARY KEY,
    ORDER_THI_IRAI_STATUS          varchar(20),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));

CREATE TABLE MS_SHOUSHURI_ITEM
(
    MS_SS_ITEM_ID                  varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ID                   int,
    ITEM_NAME                      varchar(500),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));

CREATE TABLE MS_SS_SHOUSAI_ITEM
(
    MS_SS_SHOUSAI_ITEM_ID          varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ID                   int,
    SHOUSAI_ITEM_NAME              varchar(500),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20));
    
CREATE TABLE OD_THI
(
    OD_THI_ID                      varchar(40) NOT NULL PRIMARY KEY,
    STATUS                         int NOT NULL,
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    MS_VESSEL_ID                   int NOT NULL,
    THI_IRAI_DATE                  timestamp,
    MS_THI_IRAI_SBT_ID             varchar(40) NOT NULL,
    MS_THI_IRAI_SHOUSAI_ID         varchar(40),
    BASHO                          varchar(20),
    NAIYOU                         varchar(50),
    BIKOU                          varchar(500),
    TEHAI_IRAI_NO                  varchar(30),
    KIBOUBI                        timestamp,
    KIBOUKOU                       varchar(30),
    THI_USER_ID                    varchar(40) NOT NULL,
    JIM_TANTOU_ID                  varchar(40),
    MS_THI_IRAI_STATUS_ID          varchar(40) NOT NULL,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    MM_FLAG                        int);

CREATE TABLE OD_THI_ITEM
(
    OD_THI_ITEM_ID                 varchar(40) NOT NULL PRIMARY KEY,
    MS_ITEM_SBT_ID                 varchar(40),
    ITEM_NAME                      varchar(500),
    BIKOU                          varchar(500),
    OD_THI_ID                      varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    HEADER                         varchar(50),
    SHOW_ORDER                     int,
    OD_ATTACH_FILE_ID              varchar(40)
    );

CREATE TABLE OD_THI_SHOUSAI_ITEM
(
    OD_THI_SHOUSAI_ITEM_ID         varchar(40) NOT NULL PRIMARY KEY,
    OD_THI_ITEM_ID                 varchar(40),
    SHOUSAI_ITEM_NAME              varchar(500),
    MS_VESSEL_ITEM_ID              varchar(40),
    MS_LO_ID                       varchar(40),
    ZAIKO_COUNT                    int,
    COUNT                          int,
    SATEISU                        int,
    MS_TANI_ID                     varchar(40),
    BIKOU                          varchar(500),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    SHOW_ORDER                     int,
    OD_ATTACH_FILE_ID              varchar(40)
);

CREATE TABLE OD_MM
(
    OD_MM_ID                       varchar(40) NOT NULL PRIMARY KEY,
    STATUS                         int,
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    OD_THI_ID                      varchar(40),
    MM_NO                          varchar(30),
    MM_DATE                        timestamp,
    MS_SHR_JOUKEN_ID               varchar(40),
    OKURISAKI                      varchar(50),
    NAIYOU                         varchar(50),
    MM_SAKUSEISHA                  varchar(40),
    MS_NYUKYO_KAMOKU_ID            varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    MM_KIGEN                       varchar(50));

CREATE TABLE OD_MK
(
    OD_MK_ID                       varchar(40) NOT NULL PRIMARY KEY,
    STATUS                         int,
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    OD_MM_ID                       varchar(40),
    MS_CUSTOMER_ID                 varchar(40),
    TANTOUSHA                      varchar(30),
    MK_DATE                        timestamp,
    NOUKI                          timestamp,
    KOUKI                          varchar(30),
    MK_NO                          varchar(30),
    MK_AMOUNT                      decimal(16,3),
    TANTOU_MAIL_ADDRESS            varchar(50),
    HACHU_DATE                     timestamp,
    HACHU_NO                       varchar(30) DEFAULT 0 NOT NULL,
    MS_NYUKYO_KAMOKU_ID            varchar(40),
    TAX                            decimal(16,3),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    AMOUNT                         decimal(16,3),
    WEB_KEY                        varchar(40),
    MK_KIGEN                       varchar(50),
    MK_YUKOU_KIGEN                 varchar(50),
    KIBOUBI                        timestamp,
    CREATE_DATE                    timestamp,
    CREATE_USER_ID                 varchar(40),
    MM_DATE                        timestamp,
    CARRIAGE                       decimal(16,3)
);

CREATE TABLE OD_JRY
(
    OD_JRY_ID                      varchar(40) NOT NULL PRIMARY KEY,
    STATUS                         int,
    CANCEL_FLAG                    int NOT NULL,
    OD_MK_ID                       varchar(40),
    JRY_DATE                       timestamp,
    JRY_NO                         varchar(30),
    AMOUNT                         decimal(16,3),
    NEBIKI_AMOUNT                  decimal(16,3),
    TAX                            decimal(16,3),
    KAMOKU_NO                      varchar(20),
    UTIWAKE_KAMOKU_NO              varchar(20),
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    CARRIAGE                       decimal(16,3)
    );

CREATE TABLE OD_JRY_ITEM
(
    OD_JRY_ITEM_ID                 varchar(40) NOT NULL PRIMARY KEY,
    OD_JRY_ID                      varchar(40),
    MS_ITEM_SBT_ID                 varchar(40),
    ITEM_NAME                      varchar(500),
    BIKOU                          varchar(500),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    HEADER                         varchar(50),
    SHOW_ORDER                     int
);

CREATE TABLE OD_JRY_SHOUSAI_ITEM
(
    OD_JRY_SHOUSAI_ITEM_ID         varchar(40) NOT NULL PRIMARY KEY,
    OD_JRY_ITEM_ID                 varchar(40),
    SHOUSAI_ITEM_NAME              varchar(500),
    MS_VESSEL_ITEM_ID              varchar(40),
    MS_LO_ID                       varchar(40),
    COUNT                          int,
    JRY_COUNT                      int,
    MS_TANI_ID                     varchar(40),
    TANKA                          decimal(16,3),
    BIKOU                          varchar(500),
    NOUHINBI                       timestamp,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS varchar(20),
    CANCEL_FLAG                    int DEFAULT 0 NOT NULL,
    SHOW_ORDER                     int
);

CREATE TABLE OD_CHOZO
(
    OD_CHOZO_ID                    varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ID                   int NOT NULL,
    NENGETSU                       varchar(6) NOT NULL,
    SHUBETSU                       int NOT NULL,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20));

CREATE TABLE OD_CHOZO_SHOUSAI
(
    OD_CHOZO_SHOUSAI_ID            varchar(40) NOT NULL PRIMARY KEY,
    OD_CHOZO_ID                    varchar(40) NOT NULL,
    MS_LO_ID                       varchar(40),
    MS_VESSEL_ITEM_ID              varchar(40),
    ITEM_NAME                      varchar(50),
    COUNT                          int NOT NULL,
    UKEIRE_NENGETSU                varchar(6),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20));

CREATE TABLE OD_HACHU_TANKA
(
    OD_HACHU_TANKA_ID              varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ITEM_ID              varchar(40),
    MS_LO_ID                       varchar(40),
    TANKA                          decimal(16,3) NOT NULL,
    TANKA_SETEIBI                  timestamp NOT NULL,
    OD_JRY_SHOUSAI_ITEM_ID         varchar(40),
    OD_SHR_SHOUSAI_ITEM_ID         varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20));
    
CREATE TABLE OD_SHR
(
    OD_SHR_ID                      varchar(40) NOT NULL,
    STATUS                         int,
    CANCEL_FLAG                    int NOT NULL,
    OD_JRY_ID                      varchar(40),
    SBT                            int,
    SHR_NO                         varchar(30),
    NAIYOU                         varchar(50),
    BIKOU                          varchar(500),
    KIKAN_SYSTEM_SHR_NO            varchar(20),
    AMOUNT                         decimal(16,3),
    NEBIKI_AMOUNT                  decimal(16,3),
    TAX                            decimal(16,3),
    SHR_IRAI_DATE                  timestamp,
    SHR_DATE                       timestamp,
    TEKIYOU                        varchar(50),
    KAMOKU_NO                      varchar(20),
    UTIWAKE_KAMOKU_NO              varchar(20),
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    SHR_TANTOU                     varchar(40),
    KEIJO_DATE                     timestamp,
    SYORI_STATUS                   varchar(2) NOT NULL,
    KIHYOUBI                       timestamp,
    CARRIAGE                       decimal(16,3)
);

CREATE TABLE OD_SHR_ITEM
(
    OD_SHR_ITEM_ID                 varchar(40) NOT NULL,
    OD_SHR_ID                      varchar(40),
    MS_ITEM_SBT_ID                 varchar(40),
    HEADER                         varchar(50),
    ITEM_NAME                      varchar(500),
    BIKOU                          varchar(500),
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    CANCEL_FLAG                    int NOT NULL,
    SHOW_ORDER                     int
);

CREATE TABLE OD_SHR_SHOUSAI_ITEM
(
    OD_SHR_SHOUSAI_ITEM_ID         varchar(40) NOT NULL,
    OD_SHR_ITEM_ID                 varchar(40),
    SHOUSAI_ITEM_NAME              varchar(500),
    MS_VESSEL_ITEM_ID              varchar(40),
    MS_LO_ID                       varchar(40),
    COUNT                          int,
    MS_TANI_ID                     varchar(40),
    TANKA                          decimal(16,3),
    BIKOU                          varchar(500),
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    CANCEL_FLAG                    int NOT NULL,
    SHOW_ORDER                     int
);

CREATE TABLE MS_LO_VESSEL
(
    MS_LO_VESSEL_ID                varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ID                   int,
    MS_LO_ID                       varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20));

CREATE TABLE MS_VESSEL_ITEM_VESSEL
(
    MS_VESSEL_ITEM_VESSEL_ID       varchar(40) NOT NULL PRIMARY KEY,
    MS_VESSEL_ID                   int,
    MS_VESSEL_ITEM_ID              varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20));

CREATE TABLE MS_VESSEL_ITEM_CATEGORY
(
    MS_VESSEL_ITEM_CATEGORY_NUMBER int NOT NULL PRIMARY KEY,
    CATEGORY_NAME                  varchar(50),
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    DELETE_FLAG                    int DEFAULT 0 NOT NULL
);
    
CREATE TABLE OD_GETSUJI_SHIME
(
    OD_GETSUJI_SHIME               varchar(40) NOT NULL PRIMARY KEY,
    NEN_GETSU                      varchar(6),
    MS_USER_ID                     varchar(40),
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    RENEW_DATE                     timestamp NOT NULL,
    TS                             varchar(20));


CREATE TABLE MS_SI_MENJOU (
 MS_SI_MENJOU_ID int NOT NULL PRIMARY KEY,
 NAME varchar(20),
 NAME_ABBR varchar(20),
 SHOW_ORDER int,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_MENJOU_KIND (
 MS_SI_MENJOU_KIND_ID int NOT NULL PRIMARY KEY,
 MS_SI_MENJOU_ID int,
 NAME varchar(20),
 NAME_ABBR varchar(20),
 SHOW_ORDER int,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_SHUBETSU (
 MS_SI_SHUBETSU_ID int NOT NULL PRIMARY KEY,
 NAME varchar(20),
 KYUUKA_FLAG int DEFAULT 0,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_SHOKUMEI (
 MS_SI_SHOKUMEI_ID int NOT NULL PRIMARY KEY,
 NAME varchar(20),
 NAME_ABBR varchar(20),
 SHOW_ORDER int,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SENIN
 (
 MS_SENIN_ID int NOT NULL PRIMARY KEY,
 MS_USER_ID varchar(40),
 MS_SI_SHOKUMEI_ID int,
 SEI varchar(20),
 MEI varchar(20),
 SEI_KANA varchar(20),
 MEI_KANA varchar(20),
 KUBUN int,
 SEX int,
 SHIMEI_CODE varchar(10),
 HOKEN_NO varchar(10),
 BIRTHDAY                       timestamp,
 NENKIN_NO                      varchar(25),
 NYUUSHA_DATE                   timestamp,
 POSTAL_NO                      varchar(8),
 GENJUUSHO                      varchar(55),
 HONSEKI                        varchar(50),
 TEL                            varchar(25),
 FAX                            varchar(25),
 MAIL                           varchar(50),
 KEITAI                         varchar(25),
 SONOTA                         varchar(500),
 PICTURE                        bytea,
 PICTURE_DATE                   timestamp,
 GAKUREKI                       varchar(50),
 ZENREKI                        varchar(50),
 SHOUKAISHA                     varchar(50),
 BANK_NAME1                     varchar(50),
 BRANCH_NAME1                   varchar(50),
 ACCOUNT_NO1                    varchar(25),
 BANK_NAME2                     varchar(50),
 BRANCH_NAME2                   varchar(50),
 ACCOUNT_NO2                    varchar(25),
 POSTAL_ACCOUNT_NO              varchar(25),
 CLOTH_UE                       varchar(25),
 CLOTH_SHITA                    varchar(25),
 CLOTH_KUTSU                    varchar(25),
 RETIRE_FLAG                    int DEFAULT 0 NOT NULL,
 RETIRE_DATE                    timestamp,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_MENJOU (
 SI_MENJOU_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SENIN_ID int,
 MS_SI_MENJOU_ID int,
 MS_SI_MENJOU_KIND_ID int,
 NO varchar(20),
 KIGEN timestamp,
 SHUTOKU_DATE timestamp,
 CHOUHYOU_FLAG int DEFAULT 0 NOT NULL,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_CARD (
 SI_CARD_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SENIN_ID int,
 MS_SI_SHUBETSU_ID int,
 MS_VESSEL_ID smallint,
 START_DATE timestamp,
 END_DATE timestamp,
 DAYS int,
 USER_KEY varchar(40),
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_KAZOKU (
 SI_KAZOKU_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SENIN_ID int,
 SEI varchar(20),
 MEI varchar(20),
 SEI_KANA varchar(20),
 MEI_KANA varchar(20),
 SEX int,
 ZOKUGARA varchar(20),
 BIRTHDAY timestamp,
 FUYOU int,
 TEL varchar(25),
 BIKOU varchar(500),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_RIREKI (
 SI_RIREKI_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SENIN_ID int,
 MS_SI_SHOKUMEI_ID int,
 RIREKI_DATE timestamp,
 HONKYU int,
 GEKKYU int,
 BIKOU varchar(500),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_HIYOU_KAMOKU (
 MS_SI_HIYOU_KAMOKU_ID int NOT NULL PRIMARY KEY,
 NAME varchar(20),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_DAIKOUMOKU (
 MS_SI_DAIKOUMOKU_ID int NOT NULL PRIMARY KEY,
 MS_SI_HIYOU_KAMOKU_ID int,
 NAME varchar(20),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_KAMOKU
(
    MS_SI_KAMOKU_ID                int NOT NULL PRIMARY KEY,
    MS_KAMOKU_ID                   int,
    KAMOKU_NAME                    varchar(30) NOT NULL,
    TAX_FLAG                       int DEFAULT 0,
    HIYOU_KIND                     int DEFAULT 0,
    USER_KEY                       varchar(40) DEFAULT 0,
    DATA_NO                        int,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    RENEW_DATE                     timestamp NOT NULL,
    DELETE_FLAG                    int DEFAULT 0 NOT NULL
);
    
    
CREATE TABLE MS_SI_MEISAI (
 MS_SI_MEISAI_ID int NOT NULL PRIMARY KEY,
 MS_SI_DAIKOUMOKU_ID int,
 MS_SI_KAMOKU_ID int,
 NAME varchar(20),
 NYUURYOKU_NO int DEFAULT 0,
 KASHIKARI_FLAG int DEFAULT 0,
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_SI_GETSUJI_SHIME_BI
(
    MS_SI_GETSUJI_SHIME_BI_ID      int NOT NULL,
    MONTH                          varchar(2),
    SHIME_BI                       int,
    SEND_FLAG                      int DEFAULT 0 NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) DEFAULT 0,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    RENEW_DATE                     timestamp NOT NULL,
    TS                             varchar(20)
);

CREATE TABLE SI_JUNBIKIN (
 SI_JUNBIKIN_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_VESSEL_ID int,
 MS_SI_HIYOU_KAMOKU_ID int,
 MS_SI_DAIKOUMOKU_ID int,
 MS_SI_MEISAI_ID int,
 MS_SI_KAMOKU_ID int,
 TOUROKU_USER_ID varchar(40),
 JUNBIKIN_DATE timestamp,
 KINGAKU_OUT int,
 TAX_OUT int,
 KINGAKU_IN int,
 TAX_IN int,
 BIKOU varchar(500),
 FURIKAE_FLAG int DEFAULT 0,
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_SOUKIN (
 SI_SOUKIN_ID varchar(40) NOT NULL PRIMARY KEY,
 SI_JUNBIKIN_ID varchar(40),
 MS_VESSEL_ID int,
 SOUKIN_USER_ID varchar(40),
 UKEIRE_USER_ID varchar(40),
 MS_CUSTOMER_ID varchar(40),
 SOUKIN_DATE timestamp,
 UKEIRE_DATE timestamp,
 SHOKUHI int,
 RYOHI int,
 SONOTAHI int,
 KINGAKU int,
 BIKOU varchar(500),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_LINK_SHOKUMEI_CARD (
 SI_LINK_SHOKUMEI_CARD_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SI_SHOKUMEI_ID int,
 SI_CARD_ID varchar(40),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_HAIJOU (
 SI_HAIJOU_ID varchar(40) NOT NULL PRIMARY KEY,
 HAISHIN_USER_ID varchar(40),
 HAISHIN_DATE timestamp,
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE SI_HAIJOU_ITEM (
 SI_HAIJOU_ITEM_ID varchar(40) NOT NULL PRIMARY KEY,
 SI_HAIJOU_ID varchar(40),
 MS_VESSEL_ID int,
 MS_SI_SHOKUMEI_ID int,
 MS_SI_SHUBETSU_ID int,
 MS_SENIN_ID int,
 ITEM_KIND int DEFAULT 0,
 WORKDAYS int,
 HOLIDAYS int,
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);


CREATE TABLE MS_KICHI (
 MS_KICHI_ID varchar(40) NOT NULL PRIMARY KEY,
 KICHI_NO varchar(4) NOT NULL,
 KICHI_NAME varchar(50) NOT NULL,
 DELETE_FLAG int NOT NULL,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_BASHO_KUBUN (
 MS_BASHO_KUBUN_ID varchar(40) NOT NULL PRIMARY KEY,
 BASHO_KUBUN_NAME varchar(50),
 DELETE_FLAG int NOT NULL,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_KANIDOUSEI_INFO_SHUBETU (
 MS_KANIDOUSEI_INFO_SHUBETU_ID varchar(40) NOT NULL PRIMARY KEY,
 KANIDOUSEI_INFO_SHUBETU_NAME varchar(50),
 DELETE_FLAG int NOT NULL,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_BASHO (
 MS_BASHO_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_BASHO_NO varchar(4),
 BASHO_NAME varchar(50),
 MS_BASHO_KUBUN_ID varchar(40),
 DELETE_FLAG int NOT NULL,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 GAICHI_FLAG int
);

CREATE TABLE PT_KANIDOUSEI_INFO (
 PT_KANIDOUSEI_INFO_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_VESSEL_ID int NOT NULL,
 EVENT_DATE timestamp NOT NULL,
 KOMA int,
 MS_BASHO_ID varchar(40),
 MS_KICHI_ID varchar(40),
 MS_KANIDOUSEI_INFO_SHUBETU_ID varchar(40),
 BIKOU varchar(500),
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENKEI_FLAG int,
 DELETE_FLAG int,
 DJ_DOUSEI_ID varchar(40),
 HONSEN_CHECK_DATE timestamp,
 MS_CARGO_ID int,
 QTTY decimal(16,3)
);

CREATE TABLE PT_ALARM_INFO
(
    PT_ALARM_INFO_ID               varchar(40) NOT NULL PRIMARY KEY,
    MS_PORTAL_INFO_SHUBETU_ID      varchar(40),
    MS_PORTAL_INFO_KOUMOKU_ID      varchar(40),
    MS_PORTAL_INFO_KUBUN_ID        varchar(40),
    SANSHOUMOTO_ID                 varchar(40),
    HAASEI_DATE                    timestamp,
    MS_VESSEL_ID                   int NOT NULL,
    YUUKOUKIGEN                    timestamp,
    NAIYOU                         varchar(200),
    SHOUSAI                        varchar(200),
    ALARM_SHOW_FLAG                int,
    ALARM_STOP_USER                varchar(40),
    ALARM_STOP_DATE                timestamp,
    DELETE_FLAG                    int NOT NULL,
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20)
);

CREATE TABLE PT_HONSENKOUSHIN_INFO
(
    PT_HONSENKOUSHIN_INFO_ID       varchar(40) NOT NULL PRIMARY KEY,
    MS_PORTAL_INFO_SHUBETU_ID      varchar(40),
    MS_PORTAL_INFO_KOUMOKU_ID      varchar(40),
    MS_PORTAL_INFO_KUBUN_ID        varchar(40),
    SANSHOUMOTO_ID                 varchar(40),
    EVENT_DATE                     timestamp,
    MS_VESSEL_ID                   int NOT NULL,
    HONSENKOUSHIN_INFO_USER        varchar(40),
    SHUBETSU                       varchar(50),
    NAIYOU                         varchar(50),
    KOUSHIN_NAIYOU                 varchar(200),
    DELETE_FLAG                    int NOT NULL,
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) NOT NULL,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20)
);

CREATE TABLE PT_JIMUSHOKOUSHIN_INFO
(
    PT_JIMUSHOKOUSHIN_INFO_ID      varchar(40) NOT NULL PRIMARY KEY,
    MS_PORTAL_INFO_SHUBETU_ID      varchar(40),
    MS_PORTAL_INFO_KOUMOKU_ID      varchar(40),
    MS_PORTAL_INFO_KUBUN_ID        varchar(40),
    SANSHOUMOTO_ID                 varchar(40),
    EVENT_DATE                     timestamp,
    MS_VESSEL_ID                   int NOT NULL,
    JIMUSHOKOUSHIN_INFO_USER       varchar(40),
    SHUBETSU                       varchar(50),
    NAIYOU                         varchar(50),
    KOUSHIN_NAIYOU                 varchar(200),
    DELETE_FLAG                    int NOT NULL,
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40) NOT NULL,
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20)
);

CREATE TABLE MS_PORTAL_INFO_SHUBETU
(
    MS_PORTAL_INFO_SHUBETU_ID      varchar(40) NOT NULL PRIMARY KEY,
    PORTAL_INFO_SYUBETU_NAME       varchar(50),
    DELETE_FLAG                    int NOT NULL,
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS							   varchar(20)
);

CREATE TABLE PT_PORTAL_INFO_FORMAT
(
    PT_PORTAL_INFO_FORMAT_ID       varchar(40) NOT NULL PRIMARY KEY,
    MS_PORTAL_INFO_SHUBETU_ID      varchar(40),
    MS_PORTAL_INFO_KOUMOKU_ID      varchar(40),
    NAIYOU                         varchar(200),
    SHOUSAI                        varchar(200),
    KIKAN                          int,
    DELETE_FLAG                    int NOT NULL,
    SEND_FLAG                      int NOT NULL,
    VESSEL_ID                      int NOT NULL,
    DATA_NO                        int,
    USER_KEY                       varchar(40),
    RENEW_DATE                     timestamp NOT NULL,
    RENEW_USER_ID                  varchar(40) NOT NULL,
    TS                             varchar(20),
    MS_PORTAL_INFO_KUBUN_ID        varchar(40)
);

CREATE TABLE SI_GETSUJI_SHIME (
 SI_GETSUJI_SHIME_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_USER_ID varchar(40),
 NEN_GETSU varchar(6),
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE SI_NENJI_SHIME (
 SI_NENJI_SHIME_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_USER_ID varchar(40),
 NEN varchar(4),
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DATA_NO
(
  ID int NOT NULL PRIMARY KEY,
  DATA_NO int DEFAULT 0 NOT NULL
);

CREATE TABLE SN_TABLE_INFO
(
  NAME varchar(40));


CREATE TABLE MS_BUMON (
 MS_BUMON_ID varchar(40) NOT NULL PRIMARY KEY,
 BUMON_NAME varchar(20),
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);
CREATE TABLE MS_USER_BUMON (
 MS_USER_BUMON_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_BUMON_ID varchar(40),
 MS_USER_ID varchar(40),
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_DM_BUNRUI
(
 MS_DM_BUNRUI_ID varchar(40) NOT NULL PRIMARY KEY,
 CODE            varchar(20) NOT NULL,
 NAME            varchar(50)  NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_DM_SHOUBUNRUI
(
 MS_DM_SHOUBUNRUI_ID varchar(40) NOT NULL PRIMARY KEY,
 CODE            varchar(20) NOT NULL,
 NAME            varchar(50)  NOT NULL,
 MS_DM_BUNRUI_ID varchar(40) NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_PUBLISHER
(
 DM_PUBLISHER_ID varchar(40) NOT NULL PRIMARY KEY,
 KOUKAI_SAKI int NOT NULL,
 MS_VESSEL_ID int,
 MS_BUMON_ID varchar(40),
 MS_USER_ID varchar(40),
 LINK_SAKI int NOT NULL,
 LINK_SAKI_ID varchar(40) NOT NULL, 
 SHOW_ORDER int DEFAULT 99 NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KOUKAI_SAKI
(
 DM_KOUKAI_SAKI_ID varchar(40) NOT NULL PRIMARY KEY,
 KOUKAI_SAKI int NOT NULL,
 MS_VESSEL_ID int,
 MS_BUMON_ID varchar(40),
 LINK_SAKI int NOT NULL,
 LINK_SAKI_ID varchar(40) NOT NULL, 
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);


CREATE TABLE MS_DM_HOUKOKUSHO
(
 MS_DM_HOUKOKUSHO_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_DM_BUNRUI_ID varchar(40) NOT NULL,
 MS_DM_SHOUBUNRUI_ID varchar(40),
 BUNSHO_NO            varchar(15) NOT NULL,
 BUNSHO_NAME          varchar(50) NOT NULL,
 SHUKI          varchar(50),
 JIKI          varchar(12) NOT NULL,
 CHECK_TARGET int DEFAULT 0 NOT NULL,
 TEMPLATE_FILE_NAME             varchar(100),
 FILE_UPDATE_DATE               timestamp,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);
CREATE TABLE MS_DM_TEMPLATE_FILE
(
 MS_DM_TEMPLATE_FILE_ID varchar(40) NOT NULL PRIMARY KEY,
 TEMPLATE_FILE_NAME             varchar(100),
 UPDATE_DATE               timestamp,
 DATA bytea not null,
 MS_DM_HOUKOKUSHO_ID            varchar(40) NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KOUBUNSHO_KISOKU
(
 DM_KOUBUNSHO_KISOKU_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_DM_BUNRUI_ID varchar(40) NOT NULL,
 MS_DM_SHOUBUNRUI_ID varchar(40),
 BUNSHO_NO            varchar(15) NOT NULL,
 BUNSHO_NAME          varchar(50) NOT NULL,
 STATUS int DEFAULT 0 NOT NULL,
 ISSUE_DATE timestamp NOT NULL,
 FILE_NAME             varchar(100),
 FILE_UPDATE_DATE               timestamp,
 BIKOU             varchar(100),
 
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);
CREATE TABLE DM_KOUBUNSHO_KISOKU_FILE
(
 DM_KOUBUNSHO_KISOKU_FILE_ID varchar(40) NOT NULL PRIMARY KEY,
 UPDATE_DATE               timestamp NOT NULL,
 FILE_NAME             varchar(100) NOT NULL,
 DATA bytea not null,
 DM_KOUBUNSHO_KISOKU_ID            varchar(40) NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KANRI_KIROKU
(
 DM_KANRI_KIROKU_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_DM_HOUKOKUSHO_ID varchar(40) NOT NULL,
 STATUS int DEFAULT 0 NOT NULL,
 JIKI_NEN int NOT NULL,
 JIKI_TUKI int NOT NULL,
 ISSUE_DATE timestamp NOT NULL,
 FILE_NAME             varchar(100),
 FILE_UPDATE_DATE               timestamp,
 BIKOU             varchar(100),
 
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KANRI_KIROKU_FILE
(
 DM_KANRI_KIROKU_FILE_ID varchar(40) NOT NULL PRIMARY KEY,
 UPDATE_DATE               timestamp NOT NULL,
 FILE_NAME             varchar(100) NOT NULL,
 DATA bytea not null,
 DM_KANRI_KIROKU_ID            varchar(40) NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KAKUNIN_JOKYO
(
 DM_KAKUNIN_JOKYO_ID varchar(40) NOT NULL PRIMARY KEY,
 VIEW_DATE               timestamp,
 KAKUNIN_DATE               timestamp,
 MS_USER_ID             varchar(40) NOT NULL,
 DOC_FLAG_CEO int DEFAULT 0 NOT NULL,
 DOC_FLAG_ADMIN int DEFAULT 0 NOT NULL,
 DOC_FLAG_MSI_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_CREW_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_TSI_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_MSI_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_CREW_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_TSI_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_OFFICER int DEFAULT 0 NOT NULL,
 DOC_FLAG_GL int DEFAULT 0 NOT NULL,
 DOC_FLAG_TL int DEFAULT 0 NOT NULL,
 KOUKAI_SAKI int NOT NULL,
 MS_VESSEL_ID int,
 MS_BUMON_ID varchar(40),
 LINK_SAKI int NOT NULL,
 LINK_SAKI_ID varchar(40) NOT NULL, 
 SHOW_ORDER int DEFAULT 99 NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_DOC_COMMENT
(
 DM_DOC_COMMENT_ID varchar(40) NOT NULL PRIMARY KEY,
 REG_DATE               timestamp NOT NULL,
 MS_USER_ID             varchar(40) NOT NULL,
 DOC_FLAG_CEO int DEFAULT 0 NOT NULL,
 DOC_FLAG_ADMIN int DEFAULT 0 NOT NULL,
 DOC_FLAG_MSI_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_CREW_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_TSI_FERRY int DEFAULT 0 NOT NULL,
 DOC_FLAG_MSI_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_CREW_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_TSI_CARGO int DEFAULT 0 NOT NULL,
 DOC_FLAG_OFFICER int DEFAULT 0 NOT NULL,
 DOC_FLAG_GL int DEFAULT 0 NOT NULL,
 DOC_FLAG_TL int DEFAULT 0 NOT NULL,
 KOUKAI_SAKI int NOT NULL,
 MS_VESSEL_ID int,
 MS_BUMON_ID varchar(40),
 LINK_SAKI int NOT NULL,
 LINK_SAKI_ID varchar(40) NOT NULL, 
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE DM_KANRYO_INFO
(
 DM_KANRYO_INFO_ID varchar(40) NOT NULL PRIMARY KEY,
 KANRYO_DATE               timestamp NOT NULL,
 MS_USER_ID             varchar(40) NOT NULL,
 LINK_SAKI int NOT NULL,
 LINK_SAKI_ID varchar(40) NOT NULL, 
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE PT_DM_ALARM_INFO
(
 PT_DM_ALARM_INFO_ID varchar(40) NOT NULL PRIMARY KEY,
 PT_ALARM_INFO_ID varchar(40) NOT NULL,
 MS_DM_HOUKOKUSHO_ID varchar(40) NOT NULL,
 JIKI_NEN int NOT NULL,
 JIKI_TUKI int NOT NULL, 
 KOUKAI_SAKI int NOT NULL,
 MS_VESSEL_ID int,
 MS_BUMON_ID varchar(40),
 DELETE_FLAG int DEFAULT 0 NOT NULL,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 TS varchar(20)
);

CREATE TABLE SN_PARAMETER
(
    PRM_1   varchar(256),
    PRM_2   varchar(256),
    PRM_3   varchar(256),
    PRM_4   varchar(256),
    PRM_5   varchar(256),
    PRM_6   varchar(256),
    PRM_7   varchar(256),
    PRM_8   varchar(256),
    PRM_9   varchar(256),
    PRM_10  varchar(256),
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);

CREATE TABLE OD_ATTACH_FILE
(
 OD_ATTACH_FILE_ID     varchar(40) NOT NULL PRIMARY KEY,
 FILE_NAME             varchar(100),
 DATA                  bytea not null,
 DELETE_FLAG           int DEFAULT 0 NOT NULL,
 SEND_FLAG             int DEFAULT 0 NOT NULL,
 VESSEL_ID             int NOT NULL,
 DATA_NO               int,
 USER_KEY varchar(40) DEFAULT 0,
 RENEW_USER_ID         varchar(40) NOT NULL,
 RENEW_DATE            timestamp NOT NULL,
 TS                    varchar(20)
);

    
CREATE TABLE MS_SI_KOUSHU (
 MS_SI_KOUSHU_ID int NOT NULL PRIMARY KEY,
 NAME varchar(50) NOT NULL,
 YUKOKIGEN_STR varchar(50),
 YUKOKIGEN_DAYS int,
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);

    
CREATE TABLE SI_KOUSHU (
 SI_KOUSHU_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_SI_KOUSHU_ID int NOT NULL,
 MS_SENIN_ID int,
 BASHO varchar(50),
 YOTEI_FROM timestamp,
 YOTEI_TO timestamp,
 JISEKI_FROM timestamp,
 JISEKI_TO timestamp,
 BIKOU varchar(500),
 USER_KEY varchar(40) DEFAULT 0,
 DATA_NO int,
 SEND_FLAG int DEFAULT 0 NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20),
 RENEW_DATE timestamp NOT NULL,
 DELETE_FLAG int DEFAULT 0 NOT NULL
);

CREATE TABLE DJ_DOUSEI (
 DJ_DOUSEI_ID varchar(40) NOT NULL PRIMARY KEY,
 MS_VESSEL_ID int NOT NULL,
 NAVICODE varchar(20), 
 VOYAGE_NO varchar(10), 
 KIKAN_RENKEI_FLAG int,
 MS_BASHO_ID varchar(40),
 MS_KICHI_ID varchar(40),
 DOUSEI_DATE timestamp NOT NULL,
 MS_KANIDOUSEI_INFO_SHUBETU_ID varchar(40),
 RECORD_DATETIME varchar(14),
 KIKAN_VOYAGE_NO varchar(10),
 KOMA_NO varchar(1),
 PLAN_NYUKO timestamp,
 PLAN_CHAKUSAN timestamp,
 PLAN_NIYAKU_START timestamp,
 PLAN_NIYAKU_END timestamp,
 PLAN_RISAN timestamp,
 PLAN_SHUKOU timestamp,
 RESULT_NYUKO timestamp,
 RESULT_CHAKUSAN timestamp,
 RESULT_NIYAKU_START timestamp,
 RESULT_NIYAKU_END timestamp,
 RESULT_RISAN timestamp,
 RESULT_SHUKOU timestamp,
 RESULT_MS_BASHO_ID varchar(40),
 RESULT_MS_KICHI_ID varchar(40),
 DAIRITEN_ID varchar(40),
 NINUSHI_ID varchar(40),
 BIKOU varchar(100),
 RESULT_DAIRITEN_ID varchar(40),
 RESULT_NINUSHI_ID varchar(40),
 RESULT_BIKOU varchar(100),
 DELETE_FLAG int,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);


CREATE TABLE DJ_DOUSEI_CARGO (
 DJ_DOUSEI_CARGO_ID varchar(40) NOT NULL PRIMARY KEY,
 DJ_DOUSEI_ID varchar(40),
 MS_CARGO_ID int NOT NULL,
 QTTY decimal(16,3),
 LINE_NO varchar(2),
 MS_DJ_TANI_ID varchar(40), 
 PLAN_RESULT_FLAG int,
 DELETE_FLAG int,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 DATA_NO int,
 USER_KEY varchar(40) NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);

CREATE TABLE MS_CARGO
(
  MS_CARGO_ID  int NOT NULL PRIMARY KEY,
  CARGO_NO varchar(10),
  CARGO_NAME varchar(50) NOT NULL,
  NINUSHI varchar(50),
  USER_KEY varchar(40) DEFAULT 0,
  DATA_NO int,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20),
  RENEW_DATE timestamp NOT NULL,
  DELETE_FLAG int DEFAULT 0 NOT NULL
);

CREATE TABLE MS_DJ_TANI
(
  MS_DJ_TANI_ID varchar(40) NOT NULL PRIMARY KEY,
  TANI_NAME varchar(5) NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);

CREATE TABLE MS_DJ_TENKOU
(
    MS_DJ_TENKOU_ID varchar(2) NOT NULL,
    TENKOU varchar(50),
  DELETE_FLAG int DEFAULT 0 NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);

CREATE TABLE MS_DJ_KAZAMUKI
(
    MS_DJ_KAZAMUKI_ID              varchar(2) NOT NULL,
    KAZAMUKI                       varchar(50),
  DELETE_FLAG int DEFAULT 0 NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);

CREATE TABLE MS_DJ_SENTAISETSUBI
(
    MS_DJ_SENTAISETSUBI_ID         varchar(2) NOT NULL,
    SENTAISETSUBI                  varchar(50),
  DELETE_FLAG int DEFAULT 0 NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);

CREATE TABLE MS_DJ_KENKOUJYOUTAI
(
    MS_DJ_KENKOUJYOUTAI_ID         varchar(2) NOT NULL,
    KENKOUJYOUTAI                  varchar(50),
  DELETE_FLAG int DEFAULT 0 NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);

CREATE TABLE DJ_DOUSEI_HOUKOKU
(
    DJ_DOUSEI_HOUKOKU_ID           varchar(40) NOT NULL,
    HOUKOKU_DATE                   timestamp,
    LEAVE_PORT_ID                  varchar(40),
    LEAVE_DATE                     timestamp,
    DESTINATION_PORT_ID            varchar(40),
    ARRIVAL_DATE                   timestamp,
    CURRENT_PLACE                  varchar(100),
    MS_DJ_TENKOU_ID                varchar(2),
    MS_DJ_KAZAMUKI_ID              varchar(2),
    FUSOKU                         varchar(10),
    NAMI                           varchar(10),
    UNERI                          varchar(10),
    SITEI                          varchar(10),
    SINRO                          varchar(10),
    SOKURYOKU                      varchar(10),
    MS_DJ_SENTAISETSUBI_ID         varchar(2),
    MS_DJ_KENKOUJYOUTAI_ID         varchar(2),
    NORIKUMIINSU                   varchar(5),
    BIKOU                          varchar(250),
  DELETE_FLAG int DEFAULT 0 NOT NULL,
  SEND_FLAG int DEFAULT 0 NOT NULL,
  VESSEL_ID int NOT NULL,
  DATA_NO int,
  USER_KEY varchar(40) DEFAULT 0,
  RENEW_DATE timestamp NOT NULL,
  RENEW_USER_ID varchar(40) NOT NULL,
  TS varchar(20)
);


INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'MS_USER');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'MS_VESSEL');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_JRY_SHOUSAI_ITEM');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_THI');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_THI_ITEM');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_THI_SHOUSAI_ITEM');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'MS_SHOUSHURI_ITEM');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'MS_SS_SHOUSAI_ITEM');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_CHOZO_SHOUSAI');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'SI_CARD');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'SI_LINK_SHOKUMEI_CARD');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'SI_JUNBIKIN');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'SI_SOUKIN');  

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'PT_KANIDOUSEI_INFO');  
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'PT_ALARM_INFO');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'PT_HONSENKOUSHIN_INFO');


INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_PUBLISHER');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_KOUKAI_SAKI');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_KANRI_KIROKU');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_KOUBUNSHO_KISOKU');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_KAKUNIN_JOKYO');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_KANRYO_INFO');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DM_DOC_COMMENT');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'PT_DM_ALARM_INFO');

INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DJ_DOUSEI');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DJ_DOUSEI_CARGO');
  
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'DJ_DOUSEI_HOUKOKU');
  
#  
# 2013年度改造追加分
#
INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'OD_JRY');


CREATE TABLE MS_TAX (
 MS_TAX_ID int NOT NULL PRIMARY KEY,
 TAX_RATE decimal(16,2) NOT NULL ,
 TAX_CODE varchar(40) default 0,
 START_DATE timestamp NOT NULL,
 
 DELETE_FLAG int,
 USER_KEY varchar(40) NOT NULL,
 DATA_NO int,
 SEND_FLAG int NOT NULL,
 VESSEL_ID int NOT NULL,
 RENEW_DATE timestamp NOT NULL,
 RENEW_USER_ID varchar(40) NOT NULL,
 TS varchar(20)
);


# 
# 2014年度改造追加分
#

ALTER TABLE MS_VESSEL_ITEM ADD ZAIKO_COUNT int
;
ALTER TABLE MS_VESSEL_ITEM ADD BIKOU varchar(500)
;
ALTER TABLE MS_VESSEL_ITEM_VESSEL ADD ZAIKO_COUNT int
;
ALTER TABLE MS_VESSEL_ITEM_VESSEL ADD BIKOU varchar(500)
;
ALTER TABLE MS_VESSEL_ITEM_VESSEL ADD SPECIFIC_FLAG int DEFAULT 0
;




# 
# 2018年度改造追加分
#

ALTER TABLE ms_vessel ADD sales_person_id character varying(40);

ALTER TABLE ms_vessel ADD marine_superintendent_id character varying(40);

ALTER TABLE ms_vessel ADD ms_crew_matrix_type_id  numeric(4,0);


CREATE TABLE ms_si_kyuyo_teate
(
  ms_si_kyuyo_teate_id numeric(9,0) NOT NULL PRIMARY KEY,
  name character varying(50) NOT NULL,
  yuko numeric(1,0) NOT NULL DEFAULT 0,
  show_order numeric(9,0),
  user_key character varying(40) DEFAULT 0 NOT NULL,
  data_no numeric(13,0),
  send_flag numeric(1,0) NOT NULL DEFAULT 0,
  vessel_id numeric(4,0) NOT NULL,
  renew_user_id character varying(40) NOT NULL,
  ts character varying(20),
  renew_date  timestamp NOT NULL,
  delete_flag numeric(1,0) NOT NULL DEFAULT 0
);


CREATE TABLE ms_si_kyuyo_teate_set
(
  ms_si_kyuyo_teate_set_id numeric(9,0) NOT NULL PRIMARY KEY,
  ms_si_kyuyo_teate_id numeric(9,0) NOT NULL,
  ms_si_shokumei_id numeric(9,0) NOT NULL,
  tanka numeric(6,0),
  user_key character varying(40) DEFAULT 0 NOT NULL,
  data_no numeric(13,0),
  send_flag numeric(1,0) NOT NULL DEFAULT 0,
  vessel_id numeric(4,0) NOT NULL,
  renew_user_id character varying(40) NOT NULL,
  ts character varying(20),
  renew_date  timestamp NOT NULL,
  delete_flag numeric(1,0) NOT NULL DEFAULT 0
);


CREATE TABLE si_kyuyo_teate
(
  si_kyuyo_teate_id character varying(40) NOT NULL PRIMARY KEY,
  ms_senin_id numeric(9,0) NOT NULL,
  ms_si_shokumei_id numeric(9,0),
  ms_vessel_id numeric(4,0),
  ym char(6),
  ms_si_kyuyo_teate_id numeric(9,0) NOT NULL,
  tanka numeric(6,0),
  start_date timestamp,
  end_date timestamp,
  days numeric(9,0) DEFAULT 0,
  honsen_kingaku numeric(9,0),
  kingaku numeric(9,0),
  cancel_flag numeric(1,0) NOT NULL DEFAULT 0,
  user_key character varying(40) DEFAULT 0 NOT NULL,
  data_no numeric(13,0),
  send_flag numeric(1,0) NOT NULL DEFAULT 0,
  vessel_id numeric(4,0) NOT NULL,
  renew_user_id character varying(40) NOT NULL,
  ts character varying(20),
  renew_date timestamp NOT NULL,
  delete_flag numeric(1,0) NOT NULL DEFAULT 0
);



INSERT INTO SN_TABLE_INFO
(
  NAME)
VALUES
(
  'SI_KYUYO_TEATE');


# 
# 
#
update ms_shoushuri_item set send_flag = 1 where send_flag = 0 and user_key is null;

update ms_ss_shousai_item set send_flag = 1 where send_flag = 0 and user_key is null;

ALTER TABLE si_junbikin ADD ms_tax_id numeric(9,0);


# 
# 1.0.0.4 - 2021/10/26
#
ALTER TABLE ms_user ADD  doc_flag_sd_manager       numeric(1,0);

update ms_user set doc_flag_sd_manager       = 0;

ALTER TABLE dm_kakunin_jokyo ADD  doc_flag_sd_manager       numeric(1,0) default 0;

ALTER TABLE dm_doc_comment ADD  doc_flag_sd_manager       numeric(1,0) default 0;



# 
# 1.0.0.7 - 2021/10/29
#
ALTER TABLE si_card ADD vessel_name character varying(30) COLLATE pg_catalog."default";
ALTER TABLE si_card ADD company_name character varying(100) COLLATE pg_catalog."default";
ALTER TABLE si_card ADD ms_crew_matrix_type_id numeric(4,0);
ALTER TABLE si_card ADD kenm_tushincyo numeric(1,0);
ALTER TABLE si_card ADD kenm_tushincyo_start timestamp without time zone;
ALTER TABLE si_card ADD kenm_tushincyo_end timestamp without time zone;



# 
# 1.0.1.0 - 2021/12/XX
#
ALTER TABLE ms_senin ADD ms_senin_company_id character varying(40);
ALTER TABLE ms_senin ADD department character varying(40);
ALTER TABLE ms_senin ADD sei_hiragana character varying(20);
ALTER TABLE ms_senin ADD mei_hiragana character varying(20);
ALTER TABLE ms_senin ADD member_of numeric(1,0);


ALTER TABLE ms_si_shubetsu ADD show_order numeric(9,0);


ALTER TABLE si_card ADD ms_si_shubetsu_shousai_id numeric(9,0) default 0;
ALTER TABLE si_card ADD labor_on_boarding numeric(1,0) default 0;
ALTER TABLE si_card ADD labor_on_disembarking numeric(1,0) default 0;

ALTER TABLE si_card ALTER COLUMN days TYPE numeric(9, 2);


ALTER TABLE si_link_shokumei_card ADD ms_si_shokumei_shousai_id numeric(9,0) default 0;


CREATE TABLE ms_si_shubetsu_shousai
(
    ms_si_shubetsu_shousai_id numeric(9,0) NOT NULL  PRIMARY KEY,
    ms_si_shubetsu_id numeric(9,0) NOT NULL,
    name character varying(20),
    code numeric(4,0) DEFAULT 0,
    ms_vessel_id numeric(4,0) DEFAULT 0,
    user_key character varying(40),
    data_no numeric(13,0),
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    ts character varying(20),
    renew_date timestamp without time zone NOT NULL,
    delete_flag numeric(1,0) NOT NULL DEFAULT 0
);


CREATE TABLE ms_si_shokumei_shousai
(
    ms_si_shokumei_shousai_id numeric(9,0) NOT NULL  PRIMARY KEY,
    ms_si_shokumei_id numeric(9,0) NOT NULL,
    name character varying(20),
    name_abbr character varying(20),
    name_eng character varying(20),
    show_order numeric(9,0) DEFAULT 0,
    user_key character varying(40),
    data_no numeric(13,0),
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    ts character varying(20),
    renew_date timestamp without time zone NOT NULL,
    delete_flag numeric(1,0) NOT NULL DEFAULT 0
);

# 
# 1.0.1.1 - 2022/01/XX
#
ALTER TABLE ms_thi_irai_sbt ADD show_order numeric(2,0) default 0;
ALTER TABLE ms_thi_irai_shousai ADD ms_thi_irai_sbt_id character varying(40);
ALTER TABLE ms_thi_irai_shousai ADD show_order numeric(2,0) default 0;

ALTER TABLE si_card ADD sign_off_reason character varying(40);

ALTER TABLE si_card ADD ms_vessel_type_id character varying(40);
ALTER TABLE si_card ADD gross_ton character varying(40);
ALTER TABLE si_card ADD navigation_area character varying(40);
ALTER TABLE si_card ADD owner_name character varying(40);

ALTER TABLE si_card ADD sign_on_basho_id character varying(40);
ALTER TABLE si_card ADD sign_off_basho_id character varying(40);
ALTER TABLE si_card ADD replacement_id character varying(40);




CREATE TABLE nbase_contract_function
(
    function_name character varying(40)  PRIMARY KEY,
    is_contract numeric(2,0) DEFAULT 0,
    delete_flag numeric(1,0) DEFAULT 0,
    send_flag numeric(1,0) DEFAULT 0,
    vessel_id numeric(4,0) DEFAULT 0,
    data_no numeric(13,0),
    user_key character varying(40),
    renew_date timestamp without time zone NOT NULL,
    renew_user_id character varying(40),
    ts character varying(20)
);

CREATE TABLE MS_ROLE
(
    ms_role_id numeric(9,0)  PRIMARY KEY,
    ms_bumon_id character varying(40),
    admin_flag numeric(1,0),
    name1 character varying(25),
    name2 character varying(25),
    name3 character varying(25),
    enable_flag numeric(1,0) DEFAULT 0,
    delete_flag numeric(1,0) DEFAULT 0,
    send_flag numeric(1,0) DEFAULT 0,
    vessel_id numeric(4,0) DEFAULT 0,
    data_no numeric(13,0),
    user_key character varying(40),
    renew_date timestamp without time zone NOT NULL,
    renew_user_id character varying(40),
    ts character varying(20)
);


ALTER TABLE si_card ADD wtm_linkage_id character varying(50);



