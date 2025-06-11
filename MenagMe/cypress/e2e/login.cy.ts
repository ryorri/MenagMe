/// <reference types="cypress" />

describe('Log in', () => {
  it('Login - pass', () => {
    cy.visit('http://localhost:5173')

    cy.get('input[id="login"]').type('jadminowy')
    cy.get('input[id="psw"]').type('JanAdminowy1!')

    cy.get('button[type="submit"]').click()

    cy.url().should('include', '/home')
  }),
    it('Login - err', () => {
      cy.visit('http://localhost:5173')

      cy.get('input[id="login"]').type('jadminowy')
      cy.get('input[id="psw"]').type('JanAdminowy1')

      cy.get('button[type="submit"]').click()

      cy.get('p[id="err"').contains('Invalid')
    })
})
