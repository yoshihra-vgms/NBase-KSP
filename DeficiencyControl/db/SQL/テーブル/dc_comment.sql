-- Table: dc_comment

-- DROP TABLE dc_comment;

CREATE TABLE dc_comment
(
  comment_id serial NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_comment PRIMARY KEY (comment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_comment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_comment
  IS 'コメントテーブル';

-- Trigger: tr_dc_comment on dc_comment

-- DROP TRIGGER tr_dc_comment ON dc_comment;

CREATE TRIGGER tr_dc_comment
  BEFORE INSERT OR UPDATE
  ON dc_comment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

