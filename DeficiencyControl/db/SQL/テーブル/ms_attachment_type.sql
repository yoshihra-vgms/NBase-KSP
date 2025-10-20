-- Table: ms_attachment_type

-- DROP TABLE ms_attachment_type;

CREATE TABLE ms_attachment_type
(
  attachment_type_id serial NOT NULL,
  attachment_type_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_attachment_type PRIMARY KEY (attachment_type_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_attachment_type
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_attachment_type
  IS '添付ファイル種別マスタ';

-- Trigger: tr_ms_attachment_type on ms_attachment_type

-- DROP TRIGGER tr_ms_attachment_type ON ms_attachment_type;

CREATE TRIGGER tr_ms_attachment_type
  BEFORE INSERT OR UPDATE
  ON ms_attachment_type
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();