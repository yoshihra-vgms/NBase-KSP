-- Table: dc_moi

-- DROP TABLE dc_moi;

CREATE TABLE dc_moi
(
  moi_id serial NOT NULL,

  ms_vessel_id numeric(4, 0),
  ms_user_id character varying(40),
  ms_basho_id character varying(40),
  terminal character varying,
  ms_regional_code character varying(10),
  date timestamp without time zone,
  receipt_date timestamp without time zone,

  observation integer,
  inspection_category_id integer,
  appointed_ms_customer_id character varying(10),
  inspection_ms_customer_id character varying(10),
  inspection_name character varying,
  attend character varying,
  remarks text,
  search_keyword text,

  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_moi PRIMARY KEY (moi_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_moi
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_moi
  IS '検船テーブル';

-- Trigger: tr_dc_moi on dc_moi

-- DROP TRIGGER tr_dc_moi ON dc_moi;

CREATE TRIGGER tr_dc_moi
  BEFORE INSERT OR UPDATE
  ON dc_moi
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

