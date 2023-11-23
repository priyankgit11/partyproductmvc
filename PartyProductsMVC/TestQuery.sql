INSERT INTO tblParty(party_name) VALUES('Priyank');
INSERT INTO tblParty(party_name) VALUES('Ravi');
INSERT INTO tblParty(party_name) VALUES('Dhara');

INSERT INTO tblProduct(product_name) VALUES('Apple');
INSERT INTO tblProduct(product_name) VALUES('Chopper');
INSERT INTO tblProduct(product_name) VALUES('jaljira');

INSERT INTO tblInvoice(party_id,grand_total) VALUES(5,100);
INSERT INTO tblInvoice(party_id,grand_total) VALUES(6,200);
INSERT INTO tblInvoice(party_id,grand_total) VALUES(7,300);

INSERT INTO tblInvoiceDetail(product_id,rate,quantity,invoice_id) VALUES(1,20,5,4);
INSERT INTO tblInvoiceDetail(product_id,rate,quantity,invoice_id) VALUES(2,20,10,5);
INSERT INTO tblInvoiceDetail(product_id,rate,quantity,invoice_id) VALUES(3,30,10,6);

SELECT * FROM tblParty;
SELECT * FROM tblProduct;
SELECT * FROM tblInvoice;
SELECT * FROM tblInvoiceDetail;

DELETE FROM tblParty WHERE id=7;