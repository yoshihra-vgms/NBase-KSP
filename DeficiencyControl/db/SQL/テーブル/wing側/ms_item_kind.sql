-- Table: ms_item_kind

-- DROP TABLE ms_item_kind;

CREATE TABLE ms_item_kind
(
  item_kind_id serial NOT NULL,
  item_kind_name character varying,
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_item_kind PRIMARY KEY (item_kind_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_item_kind
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_item_kind
  IS '検査種別マスタ';

-- Trigger: tr_ms_item_kind on ms_item_kind

-- DROP TRIGGER tr_ms_item_kind ON ms_item_kind;

CREATE TRIGGER tr_ms_item_kind
  BEFORE INSERT OR UPDATE
  ON ms_item_kind
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

