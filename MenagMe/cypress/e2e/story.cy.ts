/// <reference types="cypress" />

describe('Story', () => {
  it('Create story', () => {
    cy.visit('http://localhost:5173')
    cy.get('button[type="submit"]').click()
    cy.get('input[id="login"]').type('jadminowy')
    cy.get('input[id="psw"]').type('JanAdminowy1!')
    cy.get('button[type="submit"]').click()
    cy.get(':nth-child(1) > .nav-link').click()
    cy.get('.p-4 > .btn').click()
    cy.get('#name').type('Test Project')
    cy.get('#desc').type('This is a test project description for story.')
    cy.get('.form-container > .btn').click()
    cy.get('tbody > tr > :nth-child(1)').contains('Test Project')
    cy.get('select').select('Test Project')
    //project created, now create story
    cy.wait(1000)
    cy.get(':nth-child(2) > .nav-link').click()
    cy.wait(1000)
    cy.get('.p-4 > .btn').click()
    cy.get('#name').type('Test Story')
    cy.get('#desc').type('This is a test story description.')
    cy.get('form > button').click()
    cy.get('tbody > tr > :nth-child(1)').contains('Test Story')
    //edit story
    cy.get(':nth-child(6) > .btn').click()
    cy.get('#name').clear().type('Updated Test Story')
    cy.get('#desc').clear().type('This is a updated test story description.')
    cy.get('#prio').select('HIGH')
    cy.get('#state').select('INPROGRESS')
    cy.get('.submit-btn').click()
    //delete story
    cy.get(':nth-child(7) > .btn').click()
    cy.get('.delete-btn').click()
    //delete project
    cy.get(':nth-child(1) > .nav-link').click()
    cy.get(':nth-child(1) > :nth-child(5) > .btn').click()
    cy.get('.delete-btn').click()
  })
})
