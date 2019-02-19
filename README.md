PW.W.1 Parser klauzul
Napisać program, który przyjmuje zbiór formuł rachunku predykatów pierwszego rzędu i
przekształca je do postaci zbioru klauzul. Wewnętrzna reprezentacja tych klauzul powinna
umożliwiać ich wygodne stosowanie.

# ClauseParser
First Order Logic: Conversion to CNF

1. Eliminate biconditionals and implications:
• Eliminate ⇔, replacing α ⇔ β with (α ⇒ β) ∧ (β ⇒ α).
• Eliminate ⇒, replacing α ⇒ β with ¬α ∨ β.
2. Move ¬ inwards:
• ¬(∀ x p) ≡ ∃ x ¬p,
• ¬(∃ x p) ≡ ∀ x ¬p,
• ¬(α ∨ β) ≡ ¬α ∧ ¬β,
• ¬(α ∧ β) ≡ ¬α ∨ ¬β,
• ¬¬α ≡ α.
3. Standardize variables apart by renaming them: each quantifier should use a different variable.
4. Skolemize: each existential variable is replaced by a Skolem constant or Skolem function of the
enclosing universally quantified variables.
• For instance, ∃x Rich(x) becomes Rich(G1) where G1 is a new Skolem constant.
• “Everyone has a heart” ∀ x P erson(x) ⇒ ∃ y Heart(y) ∧ Has(x, y)
becomes ∀ x P erson(x) ⇒ Heart(H(x)) ∧ Has(x, H(x)),
where H is a new symbol (Skolem function).
5. Drop universal quantifiers
• For instance, ∀ x P erson(x) becomes P erson(x).
6. Distribute ∧ over ∨:
• (α ∧ β) ∨ γ ≡ (α ∨ γ) ∧ (β ∨ γ).
